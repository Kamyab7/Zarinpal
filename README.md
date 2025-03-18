# Zarinpal Client SDK

A .NET client SDK for integrating Zarinpal payment gateway into your applications.

[![Build](https://github.com/kamyab7/Zarinpal/actions/workflows/ci.yml/badge.svg)](https://github.com/kamyab7/Zarinpal/actions/workflows/ci.yml)

## Features

- Easy integration with Zarinpal payment gateway
- Support for both Sandbox and Production environments
- Async/await support for all operations
- Strongly typed request/response models
- Dependency injection support
- Built with .NET 9.0+

## Quick Start

1. Register the Zarinpal client in your dependency injection container:

```csharp
services.AddZarinpalClient(new ZarinpalClientSettings
{
    MerchantId = "your-merchant-id",
    PaymentCallBackUrl = "https://your-domain.com/callback",
    PaymentMode = PaymentMode.Sandbox // or PaymentMode.Production
});
```

2. Inject and use the ZarinpalClientService in your application:

```csharp
public class PaymentController
{
    private readonly ZarinpalClientService _zarinpalClient;

    public PaymentController(ZarinpalClientService zarinpalClient)
    {
        _zarinpalClient = zarinpalClient;
    }

    public async Task<IActionResult> CreatePayment()
    {
        var request = new PaymentRequest
        {
            Amount = 1000, // Amount in Toman
            Description = "Payment for order #123",
            CallbackUrl = "https://your-domain.com/callback"
        };

        var response = await _zarinpalClient.CreatePaymentRequest(request);
        
        if (response?.Data?.Code == 100)
        {
            return Redirect($"https://sandbox.zarinpal.com/pg/StartPay/{response.Data.Authority}");
        }

        return BadRequest(response?.Errors);
    }

    public async Task<IActionResult> VerifyPayment(string authority, string status)
    {
        if (status != "OK")
            return BadRequest("Payment was not successful");

        var request = new VerifyRequest
        {
            Amount = 1000, // Amount in Toman
            Authority = authority
        };

        var response = await _zarinpalClient.VerifyPayment(request);
        
        if (response?.Data?.Code == 100)
        {
            // Payment was successful
            return Ok(response.Data);
        }

        return BadRequest(response?.Errors);
    }
}
```

## Configuration

The `ZarinpalClientSettings` class requires the following properties:

- `MerchantId`: Your Zarinpal merchant ID
- `PaymentCallBackUrl`: The URL where Zarinpal will redirect after payment
- `PaymentMode`: Either `Sandbox` or `Production`

## Development

### Prerequisites

- .NET 9.0 SDK or later
- Visual Studio 2022 or later / VS Code with C# extension

### Building

```bash
dotnet build
```

### Running Tests

```bash
dotnet test
```

## Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Support

If you encounter any issues or have questions, please file an issue on the GitHub repository.