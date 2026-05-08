namespace LearnItAllApi.Infrastructure1.FirebaseServices;

public interface IFirebaseCfg
{
    string ApiKey        { get; init; }
    string AuthDomain    { get; init; }
    string DatabaseUrl   { get; init; }
    string StorageBucket { get; init; }
}
