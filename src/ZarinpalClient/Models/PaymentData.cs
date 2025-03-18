using System.Text.Json.Serialization;

namespace ZarinpalClient.Models;

public class PaymentData
{
    [JsonPropertyName("authority")]
    public string Authority { get; set; } = null!;


    [JsonPropertyName("fee")]
    public int Fee { get; set; }


    [JsonPropertyName("fee_type")]
    public string FeeType { get; set; } = null!;


    [JsonPropertyName("code")]
    public int Code { get; set; } 


    [JsonPropertyName("message")]
    public string Message { get; set; } = null!;
}