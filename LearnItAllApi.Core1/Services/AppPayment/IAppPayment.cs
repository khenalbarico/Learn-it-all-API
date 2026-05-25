using LearnItAllApi.Infrastructure1.ApiRelayer;
using LearnItAllApi.Infrastructure1.GoogleServices.Billing;

namespace LearnItAllApi.Core1.Services.AppPayment;

public interface IAppPayment
{
    [RelayAuthorize]
    Task<PurchaseRes> VerifyPurchaseAsync(VerifyPurchaseReq req, CancellationToken ct = default);

    [RelayAuthorize]
    Task<bool> AcknowledgeAsync(AcknowledgeReq req, CancellationToken ct = default);
}