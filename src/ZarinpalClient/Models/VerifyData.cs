using System.Text.Json.Serialization;

namespace ZarinpalClient.Models;

public class VerifyData
{
    [JsonPropertyName("wages")]
    public string Wages { get; set; } = null!;
    
    [JsonPropertyName("code")]
    public int Code { get; set; }
    
    [JsonPropertyName("messages")]
    public string Message { get; set; } = null!;
    
    [JsonPropertyName("card_hash")]
    public string CardHash { get; set; } = null!;
    
    [JsonPropertyName("card_pan")]
    public string CardPan { get; set; } = null!;
    
    [JsonPropertyName("ref_id")]
    public long RefId { get; set; }
    
    [JsonPropertyName("fee_type")]
    public string FeeType { get; set; } = null!;
    
    [JsonPropertyName("fee")]
    public int Fee { get; set; }
    
    [JsonPropertyName("shaparak_fee")]
    public int ShaparakFee { get; set; }
    
    [JsonPropertyName("order_id")]
    public string OrderId { get; set; } = null!;
}