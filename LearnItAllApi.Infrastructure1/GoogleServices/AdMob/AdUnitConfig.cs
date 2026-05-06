namespace LearnItAllApi.Infrastructure1.GoogleServices.AdMob;

public sealed class AdUnitConfig
{
    public string AppId      { get; init; } = string.Empty;
    public string AdUnitId   { get; init; } = string.Empty;
    public bool   IsTestMode { get; init; }
}