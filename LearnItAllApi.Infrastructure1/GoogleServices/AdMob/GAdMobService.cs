namespace LearnItAllApi.Infrastructure1.GoogleServices.AdMob;

public sealed class GAdMobService(IGAdMobCfg _cfg) : IGAdMobService
{
    private static class TestIds
    {
        public const string Banner       = "ca-app-pub-3940256099942544/6300978111";
        public const string Interstitial = "ca-app-pub-3940256099942544/1033173712";
        public const string Rewarded     = "ca-app-pub-3940256099942544/5224354917";
        public const string Native       = "ca-app-pub-3940256099942544/2247696110";
    }

    public AdUnitConfig GetBanner()       => Resolve(_cfg.BannerAdUnitId, TestIds.Banner);
    public AdUnitConfig GetInterstitial() => Resolve(_cfg.InterstitialAdUnitId, TestIds.Interstitial);
    public AdUnitConfig GetRewarded()     => Resolve(_cfg.RewardedAdUnitId, TestIds.Rewarded);
    public AdUnitConfig GetNative()       => Resolve(_cfg.NativeAdUnitId, TestIds.Native);

    private AdUnitConfig Resolve(string productionId, string testId) => new()
    {
        AppId      = _cfg.AppId,
        AdUnitId   = _cfg.TestMode ? testId : productionId,
        IsTestMode = _cfg.TestMode
    };
}