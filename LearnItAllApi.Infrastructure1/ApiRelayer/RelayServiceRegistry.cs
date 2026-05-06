namespace LearnItAllApi.Infrastructure1.ApiRelayer;

public sealed class RelayServiceRegistry
{
    private readonly Dictionary<string, Type> _services = new(StringComparer.Ordinal);

    public void Add<TService>()       => _services[typeof(TService).Name] = typeof(TService);

    public Type Get(string className) =>
        _services.TryGetValue(className, out var type)
            ? type
            : throw new KeyNotFoundException($"Service '{className}' is not registered.");
}