using LearnItAllApi.Infrastructure1.FirebaseServices;

namespace TestProject1.TestTools;

public class TestCfg : IFirebaseCfg
{
    public string ApiKey                       { get; init; } = string.Empty;
    public string AuthDomain                   { get; init; } = string.Empty;
    public string DatabaseUrl                  { get; init; } = string.Empty;
    public string StorageBucket                { get; init; } = string.Empty;
    public string ProjectId                    { get; init; } = string.Empty;
    public string GoogleApplicationCredentials { get; init; } = string.Empty;
}
