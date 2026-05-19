using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Database;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;

namespace LearnItAllApi.Infrastructure1.FirebaseServices;

public static class FirebaseClientFactory
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
        var credential = ResolveCredential(cfg.GoogleApplicationCredentials)
            .CreateScoped("https://www.googleapis.com/auth/cloud-platform",
                          "https://www.googleapis.com/auth/datastore");

        var firestoreClient = new FirestoreClientBuilder
        {
            Credential = credential
        }.Build();

        return FirestoreDb.Create(cfg.ProjectId, firestoreClient);
    }

    public static GoogleCredential ResolveCredential(string value) =>
        value.TrimStart().StartsWith('{')
            ? CredentialFactory.FromJson<ServiceAccountCredential>(value).ToGoogleCredential()
            : CredentialFactory.FromFile<ServiceAccountCredential>(value).ToGoogleCredential();
}
