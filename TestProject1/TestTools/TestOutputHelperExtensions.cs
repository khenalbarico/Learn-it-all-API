using FirebaseAdmin;
using LearnItAllApi.Core1;
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
using Microsoft.Extensions.Hosting;
using Xunit.Abstractions;

namespace TestProject1.TestTools;

internal static class TestOutputHelperExtensions
{
    static IHost? Host;

    internal static T Get<T>(this ITestOutputHelper ctx) where T : class
    {
        Host ??= new HostBuilder()
            .ConfigureAppConfiguration((_, cfgBuilder) =>
            {
                cfgBuilder
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("testsettings.json", optional: false);
            })
            .ConfigureServices((hostCtx, svc) =>
            {
                var cfg = hostCtx.Configuration;

                AddTestCfg(svc, cfg);
                InitFirebase(cfg);
                AddSvcRegistry(svc);
                AddRelayServices(svc);
            })
            .Build();

        return Host.Services.GetRequiredService<T>();
    }

    static void AddTestCfg(IServiceCollection svc, IConfiguration cfg)
    {
        var testCfg = new TestCfg();
        cfg.GetSection("Firebase").Bind(testCfg);
        cfg.GetSection("GoogleAdMob").Bind(testCfg);
        cfg.GetSection("GoogleBilling").Bind(testCfg);

        svc.AddSingleton<IGAdMobCfg>(testCfg);
        svc.AddSingleton<IGBillingCfg>(testCfg);
        svc.AddSingleton<IFirebaseCfg>(testCfg);
    }

    static void InitFirebase(IConfiguration cfg)
    {
        if (FirebaseApp.DefaultInstance != null) return;

        var testCfg = new TestCfg();
        cfg.GetSection("Firebase").Bind(testCfg);

        FirebaseApp.Create(new AppOptions
        {
            Credential = FirebaseClientFactory.ResolveCredential(testCfg.GoogleApplicationCredentials)
        });
    }

    static void AddSvcRegistry(IServiceCollection svc)
    {
        svc.AddHttpClient();
        svc.AddSingleton<IFirebaseAuth, FirebaseAuth>();
        svc.AddSingleton<IFirebaseRealtimeDb, FirebaseRealtimeDb1>();
        svc.AddSingleton<IFirebaseStorage, FirebaseStorage>();
        svc.AddSingleton<IFirebaseFirestoreDb, FirebaseFirestoreDb>();
    }

    static void AddRelayServices(IServiceCollection svc)
    {
        var relayRegistry = new RelayServiceRegistry();
        svc.AddSingleton(relayRegistry);

        svc.AddRelaySingleton<IAppAuth, AppAuth>(relayRegistry);
        svc.AddRelaySingleton<IAppRepository, AppRepository>(relayRegistry);
        svc.AddRelaySingleton<IAppStorage, AppStorage>(relayRegistry);
    }
}