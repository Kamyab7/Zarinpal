namespace ZarinpalClient;

public class ZarinpalClientSettings
{
    public string MerchantId { get; init; } = null!;

    public string PaymentCallBackUrl { get; init; } = null!;

    public PaymentMode PaymentMode { get; init; }
}