using Google.Apis.AndroidPublisher.v3.Data;

namespace LearnItAllApi.Infrastructure1.GoogleServices.Billing;

public sealed class GBillingService(IGBillingCfg _cfg) : IGBillingService
{
    public async Task<PurchaseRes> VerifyPurchaseAsync(VerifyPurchaseReq req, CancellationToken ct = default)
    {
        using var client   = _cfg.CreateClient();
              var purchase = await client.Purchases.Products
                  .Get(_cfg.PackageName, req.ProductId, req.PurchaseToken)
                  .ExecuteAsync(ct);

        return new PurchaseRes
        {
            IsValid          = purchase.PurchaseState        == 0,
            IsAcknowledged   = purchase.AcknowledgementState == 1,
            OrderId          = purchase.OrderId,
            PurchaseTimeMs   = purchase.PurchaseTimeMillis   ?? 0,
            ConsumptionState = purchase.ConsumptionState     ?? 0
        };
    }

    public async Task<bool> AcknowledgeAsync(AcknowledgeReq req, CancellationToken ct = default)
    {
        using var client = _cfg.CreateClient();
              var body   = new ProductPurchasesAcknowledgeRequest
              {
                  DeveloperPayload = req.DeveloperPayload
              };

        await client.Purchases.Products
            .Acknowledge(body, _cfg.PackageName, req.ProductId, req.PurchaseToken)
            .ExecuteAsync(ct);

        return true;
    }
}