using System.Text;
using System.Text.Json;
using ZarinpalClient.Models;

namespace ZarinpalClient;

public class ZarinpalClientService
{
    private readonly HttpClient _httpClient = new();
    public ZarinpalClientService(ZarinpalClientSettings zarinpalClientSettings)
    {
        var baseAddress= zarinpalClientSettings.PaymentMode==PaymentMode.Sandbox 
            ? "https://sandbox.zarinpal.com/" 
            : "https://payment.zarinpal.com/";
        _httpClient.BaseAddress = new Uri(baseAddress);
    }

    public async Task<ZarinpalResponse<PaymentData>?> CreatePaymentRequest(PaymentRequest request)
    {
        var requestBody = JsonSerializer.Serialize(request);
        
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, "pg/v4/payment/request.json")
        {
            Headers =
            {
                { "accept", "application/json" }
            },
            Content = new StringContent(requestBody, Encoding.UTF8, "application/json")
        };
        
        var response = await _httpClient.SendAsync(httpRequest);
        
        var responseContent = await response.Content.ReadAsStringAsync();
        
        response.EnsureSuccessStatusCode();
        
        return JsonSerializer.Deserialize<ZarinpalResponse<PaymentData>>(responseContent);
    }

    public async Task<ZarinpalResponse<VerifyData>?> VerifyPayment(VerifyRequest request)
    {
        var requestBody = JsonSerializer.Serialize(request);
        
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, "pg/v4/payment/verify.json")
        {
            Headers =
            {
                { "accept", "application/json" },
            },
            Content = new StringContent(requestBody, Encoding.UTF8, "application/json")
        };

        var response = await _httpClient.SendAsync(httpRequest);
        
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<ZarinpalResponse<VerifyData>>(responseContent);
    }
}