using System.Text.Json.Serialization;

namespace ZarinpalClient.Models;

public class VerifyRequest
{
    [JsonPropertyName("merchant_id")]
    public string MerchantId { get; set; } = null!;
    [JsonPropertyName("amount")]
    public int Amount { get; set; }
    [JsonPropertyName("authority")]
    public string Authority { get; set; } = null!;
}