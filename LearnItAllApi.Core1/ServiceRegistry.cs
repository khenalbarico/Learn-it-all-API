using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using LearnItAllApi.Core1.Services.AppAuthentication;
using LearnItAllApi.Core1.Services.AppRepository;
using LearnItAllApi.Core1.Services.AppStorage;
using LearnItAllApi.Infrastructure1.ApiRelayer;
using LearnItAllApi.Infrastructure1.FirebaseServices;
using LearnItAllApi.Infrastructure1.FirebaseServices.Authentication;
using LearnItAllApi.Infrastructure1.FirebaseServices.FirestoreDatabase;
using LearnItAllApi.Infrastructure1.FirebaseServices.FireStoreDatabase;
using LearnItAllApi.Infrastructure1.FirebaseServices.RealtimeDatabase;
using LearnItAllApi.Infrastructure1.FirebaseServices.Storage;
using LearnItAllApi.Infrastructure1.GoogleServices.AdMob;
using LearnItAllApi.Infrastructure1.GoogleServices.Billing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LearnItAllApi.Core1;

public static class ServiceRegistry
{
    public static void RegisterServices(this IServiceCollection svc, IConfiguration cfg)
    {
        AddAppCfg(svc, cfg);
        InitFirebase(cfg);
        AddSvcRegistry(svc);
        AddRelayServices(svc);
    }

    public static void AddAppCfg(IServiceCollection svc, IConfiguration cfg)
    {
        var appCfg = new AppCfg();

        cfg.GetSection("GoogleAdMob").Bind(appCfg);
        cfg.GetSection("GoogleBilling").Bind(appCfg);
        cfg.GetSection("Firebase").Bind(appCfg);

        svc.AddSingleton<IGAdMobCfg>(appCfg);
        svc.AddSingleton<IGBillingCfg>(appCfg);
        svc.AddSingleton<IFirebaseCfg>(appCfg);
    }

    public static void InitFirebase(IConfiguration cfg)
    {
        if (FirebaseApp.DefaultInstance != null) return;

        var appCfg = new AppCfg();
        cfg.GetSection("Firebase").Bind(appCfg);

        FirebaseApp.Create(new AppOptions
        {
            Credential = CredentialFactory
                         .FromFile<ServiceAccountCredential>(appCfg.GoogleApplicationCredentials)
                         .ToGoogleCredential()
        });
    }

    public static void AddSvcRegistry(IServiceCollection svc)
    {
        svc.AddHttpClient();
        svc.AddSingleton<IApiRelay, ApiRelay>();
        svc.AddSingleton<IRelayDispatcher, RelayDispatcher>();
        svc.AddSingleton<IFirebaseTokenVerifier, FirebaseTokenVerifier>();

        svc.AddSingleton<IGAdMobService, GAdMobService>();
        svc.AddSingleton<IGBillingService, GBillingService>();

        svc.AddSingleton<IFirebaseAuth, FirebaseAuth>();
        svc.AddSingleton<IFirebaseRealtimeDb, FirebaseRealtimeDb1>();
        svc.AddSingleton<IFirebaseStorage, FirebaseStorage>();
        svc.AddSingleton<IFirebaseFirestoreDb, FirebaseFirestoreDb>();
    }

    public static void AddRelayServices(IServiceCollection svc)
    {
        var relayRegistry = new RelayServiceRegistry();
        svc.AddSingleton(relayRegistry);

        svc.AddRelaySingleton<IAppAuth, AppAuth>(relayRegistry);
        svc.AddRelaySingleton<IAppRepository, AppRepository>(relayRegistry);
        svc.AddRelaySingleton<IAppStorage, AppStorage>(relayRegistry);
    }
}
