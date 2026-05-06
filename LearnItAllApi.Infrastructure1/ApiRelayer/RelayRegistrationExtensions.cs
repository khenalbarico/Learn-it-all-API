using Microsoft.Extensions.DependencyInjection;

namespace LearnItAllApi.Infrastructure1.ApiRelayer;

public static class RelayRegistrationExtensions
{
    public static IServiceCollection AddRelaySingleton<TService, TImplementation>(
           this  IServiceCollection      services,
                 RelayServiceRegistry    registry) 
           where TService        : class 
           where TImplementation : class, TService
    {
        services.AddSingleton<TService, TImplementation>();

        registry.Add<TService>();

        return services;
    }
}