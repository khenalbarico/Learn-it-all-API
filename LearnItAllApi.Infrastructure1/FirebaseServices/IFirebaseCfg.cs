namespace LearnItAllApi.Infrastructure1.FirebaseServices;

public interface IFirebaseCfg
{
    string ApiKey      { get; set; }
    string AuthDomain  { get; set; }
    string DatabaseUrl { get; set; }
}
