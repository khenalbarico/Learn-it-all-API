using LearnItAllApi.Core1.AppAuthentication;
using LearnItAllApi.Core1.AppRepository;
using LearnItAllApi.Core1.AppStorageService;
using LearnItAllApi.Infrastructure1.ApiRelayer;
using LearnItAllApi.Infrastructure1.FirebaseServices;
using LearnItAllApi.Infrastructure1.FirebaseServices.Authentication;
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

    public static void AddSvcRegistry(IServiceCollection svc)
    {
        svc.AddHttpClient();
        svc.AddSingleton<IApiRelay, ApiRelay>();
        svc.AddSingleton<IRelayDispatcher, RelayDispatcher>();

        //Google Services
        svc.AddSingleton<IGAdMobService, GAdMobService>();
        svc.AddSingleton<IGBillingService, GBillingService>();

        //Firebase Services
        svc.AddSingleton<IFirebaseAuth, FirebaseAuth>();
        svc.AddSingleton<IFirebaseRealtimeDb, FirebaseRealtimeDb1>();
        svc.AddSingleton<IFirebaseStorage, FirebaseStorage>();
    }

    public static void AddRelayServices(IServiceCollection svc)
    {
        var relayRegistry = new RelayServiceRegistry();
        svc.AddSingleton(relayRegistry);

        svc.AddRelaySingleton<IAppAuth, AppAuth>(relayRegistry);
        svc.AddRelaySingleton<IAppRepos, AppRepos>(relayRegistry);
        svc.AddRelaySingleton<IAppStorage, AppStorage>(relayRegistry);
    }
}
