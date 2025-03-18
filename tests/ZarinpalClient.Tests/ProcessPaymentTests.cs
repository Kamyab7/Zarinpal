using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright;
using ZarinpalClient.Models;

namespace ZarinpalClient.Tests;

public class Tests
{
    private ServiceProvider _serviceProvider;
    private IBrowser _browser;
    private IBrowserContext _context;
    private IPage _page;
    
    [SetUp]
    public async Task Setup()
    {
        var playwright = await Playwright.CreateAsync();

        _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true,
            SlowMo = 1000
        });

        _context = await _browser.NewContextAsync();
        _page = await _context.NewPageAsync();
        
        var serviceCollection = new ServiceCollection();
        var settings = new ZarinpalClientSettings()
        {
            MerchantId = Environment.GetEnvironmentVariable("ZARINPAL_MERCHANT_ID") ?? throw new InvalidOperationException("ZARINPAL_MERCHANT_ID environment variable is not set"),
            PaymentMode = PaymentMode.Sandbox,
            PaymentCallBackUrl = "www.zarinpal-client-test.com"
        };
        
        serviceCollection.AddZarinpalClient(settings);

        _serviceProvider = serviceCollection.BuildServiceProvider();
    }
    
    [TearDown]
    public async Task TearDown()
    {
        await _browser.CloseAsync();
        _serviceProvider?.Dispose();
    }

    [Test]
    public async Task PaymentClientServiceShouldProcessPayment()
    {
        var paymentService = _serviceProvider.GetRequiredService<ZarinpalClientService>();
        var settings= _serviceProvider.GetRequiredService<ZarinpalClientSettings>();
        var paymentRequestResult = await paymentService.CreatePaymentRequest(new PaymentRequest()
        {
            Amount = 1000,
            Description = "Test Payment",
            Metadata = new Metadata()
            {
                Email = "test@test.com",
                Mobile = "123456789",
            },
            MerchantId = settings.MerchantId,
            CallbackUrl = settings.PaymentCallBackUrl
        });
        
        await _page.GotoAsync($"https://sandbox.zarinpal.com/pg/StartPay/{paymentRequestResult?.Data?.Authority}");
        await _page.ClickAsync(".buttons-submit"); //buttons-cancel
        
        var verificationResult = await paymentService.VerifyPayment(new VerifyRequest()
        {
            Amount = 1000,
            Authority = paymentRequestResult?.Data?.Authority!,
            MerchantId = settings.MerchantId,
        });

        paymentRequestResult.Should().NotBeNull();
        paymentRequestResult!.Data!.Authority.Should().NotBeNull();
        paymentRequestResult.Data.Code.Should().Be(100);
        paymentRequestResult.Data.Fee.Should().Be(1000);
        paymentRequestResult.Data.Message.Should().Be("Success");
        paymentRequestResult.Data.FeeType.Should().Be("Merchant");
        paymentRequestResult.Errors!.Count().Should().Be(0);
        verificationResult.Should().NotBeNull();
        verificationResult.Data?.Fee.Should().Be(1000);
        verificationResult.Errors!.Count().Should().Be(0);
    }
}
