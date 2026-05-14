using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Database;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;

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

    public static FirebaseClient CreateDbClient(this IFirebaseCfg cfg)
    {
        return new FirebaseClient(cfg.DatabaseUrl);
    }

    public static FirestoreDb CreateFirestoreClient(this IFirebaseCfg cfg)
    {
        return FirestoreDb.Create(cfg.ProjectId);
    }

    public static FirestoreDb CreateFirestoreClient(this IFirebaseCfg cfg, string idToken)
    {
        var credential      = GoogleCredential.FromAccessToken(idToken);
        var firestoreClient = new FirestoreClientBuilder
        {
            Credential = credential
        }.Build();

        return FirestoreDb.Create(cfg.ProjectId, firestoreClient);
    }
}