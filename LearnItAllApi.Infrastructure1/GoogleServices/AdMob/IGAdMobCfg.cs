namespace LearnItAllApi.Infrastructure1.GoogleServices.AdMob;

public interface IGAdMobCfg
{
    string AppId                { get; }
    string BannerAdUnitId       { get; }
    string InterstitialAdUnitId { get; }
    string RewardedAdUnitId     { get; }
    string NativeAdUnitId       { get; }
    bool   TestMode             { get; }
}