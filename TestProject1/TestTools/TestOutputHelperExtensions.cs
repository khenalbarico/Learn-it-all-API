using LearnItAllApi.Core1;
using LearnItAllApi.Infrastructure1.FirebaseServices;
using LearnItAllApi.Infrastructure1.FirebaseServices.Authentication;
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
            .ConfigureAppConfiguration((hostCtx, cfgBuilder) =>
            {
                cfgBuilder
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("testsettings.json", optional: false);
            })
            .ConfigureServices((hostCtx, svc) =>
            {
                svc.AddHttpClient();

                var testCfg = new TestCfg();

                hostCtx.Configuration.GetSection("Firebase").Bind(testCfg);
                svc.AddSingleton<IFirebaseCfg>(testCfg);

                svc.AddSingleton<IFirebaseAuth, FirebaseAuth>();
            })
            .Build();

        return Host.Services.GetRequiredService<T>();
    }
}