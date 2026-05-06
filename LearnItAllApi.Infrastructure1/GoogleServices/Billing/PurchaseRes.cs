namespace LearnItAllApi.Infrastructure1.GoogleServices.Billing;

public sealed class PurchaseRes
{
    public bool   IsValid          { get; init; }
    public bool   IsAcknowledged   { get; init; }
    public string OrderId          { get; init; } = string.Empty;
    public long   PurchaseTimeMs   { get; init; }
    public int    ConsumptionState { get; init; }
}
