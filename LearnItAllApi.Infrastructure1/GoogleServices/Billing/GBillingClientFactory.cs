using Google.Apis.AndroidPublisher.v3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;

namespace LearnItAllApi.Infrastructure1.GoogleServices.Billing;

internal static class GBillingClientFactory
{
    public static AndroidPublisherService CreateClient(this IGBillingCfg cfg)
    {
        var credential = CredentialFactory
            .FromJson<ServiceAccountCredential>(cfg.ServiceAccountJson)
            .ToGoogleCredential()
            .CreateScoped(AndroidPublisherService.Scope.Androidpublisher);

        return new AndroidPublisherService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credential,
            ApplicationName       = cfg.PackageName
        });
    }
}