using LearnItAllApi.Infrastructure1.GoogleServices.Billing;

namespace LearnItAllApi.Core1.Services.AppPayment;

public class AppPayment(IGBillingService _gBillService) : IAppPayment
{
    public async Task<PurchaseRes> VerifyPurchaseAsync(VerifyPurchaseReq req, CancellationToken ct = default)
    {
        try
        {
            return await _gBillService.VerifyPurchaseAsync(req, ct);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to verify purchase.", ex);
        }
    }

    public async Task<bool> AcknowledgeAsync(AcknowledgeReq req, CancellationToken ct = default)
    {
        try
        {
            return await _gBillService.AcknowledgeAsync(req, ct);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to acknowledge purchase.", ex);
        }
    }
}