using System.Text.Json.Serialization;

namespace ZarinpalClient.Models;

public class ZarinpalResponse<T> where T : class
{
    [JsonPropertyName("data")]
    public T? Data { get; init; }

    [JsonPropertyName("errors")]
    public List<string>? Errors { get; init; } = new();
}