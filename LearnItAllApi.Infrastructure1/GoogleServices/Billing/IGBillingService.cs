namespace LearnItAllApi.Infrastructure1.GoogleServices.Billing;

public interface IGBillingService
{
    Task<PurchaseRes> VerifyPurchaseAsync(VerifyPurchaseReq req, CancellationToken ct = default);
    Task<bool> AcknowledgeAsync(AcknowledgeReq request, CancellationToken ct = default);
}
