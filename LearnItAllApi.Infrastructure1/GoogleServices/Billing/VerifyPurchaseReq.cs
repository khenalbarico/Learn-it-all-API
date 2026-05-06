namespace LearnItAllApi.Infrastructure1.GoogleServices.Billing;

public sealed class VerifyPurchaseReq
{
    public string ProductId     { get; init; } = string.Empty;
    public string PurchaseToken { get; init; } = string.Empty;
}
