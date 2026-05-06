namespace LearnItAllApi.Infrastructure1.GoogleServices.AdMob;

public interface IGAdMobService
{
    AdUnitConfig GetBanner();
    AdUnitConfig GetInterstitial();
    AdUnitConfig GetRewarded();
    AdUnitConfig GetNative();
}