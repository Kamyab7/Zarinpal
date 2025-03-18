using System.Text.Json.Serialization;

namespace ZarinpalClient.Models;

public class PaymentRequest
{
    [JsonPropertyName("merchant_id")]
    public string MerchantId { get; set; } = null!;

    [JsonPropertyName("amount")]
    public int Amount { get; set; }

    [JsonPropertyName("callback_url")]
    public string CallbackUrl { get; set; } = null!;

    [JsonPropertyName("description")]
    public string Description { get; set; } = null!;

    [JsonPropertyName("metadata")]
    public Metadata? Metadata { get; set; } 
}