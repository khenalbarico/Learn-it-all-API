using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Database;
using Google.Cloud.Firestore;

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

    public static FirebaseClient CreateDbClient(this IFirebaseCfg cfg)
    {
        return new FirebaseClient(cfg.DatabaseUrl);
    }

    public static FirestoreDb CreateFirestoreClient(this IFirebaseCfg cfg)
    {
        return FirestoreDb.Create(cfg.ProjectId);
    }
}