using LearnItAllApi.Core1.AppAuthentication;
using LearnItAllApi.Core1.AppRepository;
using LearnItAllApi.Infrastructure1.ApiRelayer;
using LearnItAllApi.Infrastructure1.FirebaseServices;
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

        svc.AddSingleton<IGAdMobCfg>(appCfg);
        svc.AddSingleton<IGBillingCfg>(appCfg);
    }

    public static void AddSvcRegistry(IServiceCollection svc)
    {
        svc.AddHttpClient();
        svc.AddSingleton<IApiRelay, ApiRelay>();
        svc.AddSingleton<IRelayDispatcher, RelayDispatcher>();
        svc.AddSingleton<IGAdMobService, GAdMobService>();
        svc.AddSingleton<IGBillingService, GBillingService>();
        svc.AddSingleton<IFirebaseSessionStore, FirebaseSessionStore>();
    }

    public static void AddRelayServices(IServiceCollection svc)
    {
        var relayRegistry = new RelayServiceRegistry();
        svc.AddSingleton(relayRegistry);

        svc.AddRelaySingleton<IAppAuth, AppAuth>(relayRegistry);
        svc.AddRelaySingleton<IAppRepos, AppRepos>(relayRegistry);
    }
}
