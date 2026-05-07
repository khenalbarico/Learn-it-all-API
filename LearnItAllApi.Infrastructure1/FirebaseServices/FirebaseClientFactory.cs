using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Database;

namespace LearnItAllApi.Infrastructure1.FirebaseServices;

internal static class FirebaseClientFactory
{
    public static FirebaseAuthClient CreateAuthClient(this IFirebaseCfg cfg)
    {
        return new FirebaseAuthClient(new FirebaseAuthConfig
        {
            ApiKey     = cfg.ApiKey,
            AuthDomain = cfg.AuthDomain,
            Providers  = [new EmailProvider()]
        });
    }

    public static FirebaseClient CreateDbClient(this IFirebaseCfg cfg, string idToken)
    {
        return new FirebaseClient(
            cfg.DatabaseUrl,
            new FirebaseOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(idToken)
            });
    }
}