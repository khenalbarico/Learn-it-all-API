namespace LearnItAllApi.Infrastructure1.GoogleServices.Billing;

public interface IGBillingCfg
{
    string PackageName        { get; }
    string ServiceAccountJson { get; }
    string PublisherAccountId { get; }
}