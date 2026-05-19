using LearnItAllApi.Infrastructure1.FirebaseServices;
using LearnItAllApi.Infrastructure1.GoogleServices.AdMob;
using LearnItAllApi.Infrastructure1.GoogleServices.Billing;

namespace LearnItAllApi.Core1;

public class AppCfg : IGAdMobCfg, IGBillingCfg, IFirebaseCfg
{
    public string AppId                        { get; init; } = string.Empty;
    public string BannerAdUnitId               { get; init; } = string.Empty;
    public string InterstitialAdUnitId         { get; init; } = string.Empty;
    public string RewardedAdUnitId             { get; init; } = string.Empty;
    public string NativeAdUnitId               { get; init; } = string.Empty;
    public bool   TestMode                     { get; init; }
    public string PackageName                  { get; init; } = string.Empty;
    public string ServiceAccountJson           { get; init; } = string.Empty;
    public string PublisherAccountId           { get; init; } = string.Empty;
    public string ApiKey                       { get; init; } = string.Empty;
    public string AuthDomain                   { get; init; } = string.Empty;
    public string DatabaseUrl                  { get; init; } = string.Empty;
    public string StorageBucket                { get; init; } = string.Empty;
    public string ProjectId                    { get; init; } = string.Empty;
    public string GoogleApplicationCredentials { get; init; } = string.Empty;
}
