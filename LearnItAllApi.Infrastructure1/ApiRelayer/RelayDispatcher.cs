using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Text.Json;

namespace LearnItAllApi.Infrastructure1.ApiRelayer;

public sealed class RelayDispatcher(
                    IServiceProvider     _services,
                    RelayServiceRegistry _registry) : IRelayDispatcher
{
    static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    public async Task<object?> DispatchAsync(RelayReq request, CancellationToken ct = default)
    {
        var serviceType = _registry.Get(request.ClassName);
        var service     = _services.GetRequiredService(serviceType);

        var method = serviceType.GetMethods()
            .FirstOrDefault(m => m.Name == request.MethodName)
            ?? throw new InvalidOperationException(
                $"Method '{request.MethodName}' not found on '{request.ClassName}'.");

        var result = method.Invoke(service, BuildArguments(method, request.Payload, ct));

        if (result is not Task task)
            return result;

        await task;

        return task.GetType().IsGenericType
            ? task.GetType().GetProperty("Result")?.GetValue(task)
            : null;
    }

    private static object?[] BuildArguments(MethodInfo method, JsonElement? payload, CancellationToken ct)
    {
        var parameters = method.GetParameters();
        if (parameters.Length == 0) return [];

        var args       = new object?[parameters.Length];
        var properties = GetPayloadProperties(payload);

        for (var i = 0; i < parameters.Length; i++)
        {
            var param = parameters[i];

            if (param.ParameterType == typeof(CancellationToken))
            {
                args[i] = ct;
                continue;
            }

            if (parameters.Length == 1)
            {
                args[i] = payload?.Deserialize(param.ParameterType, JsonOptions);
                continue;
            }

            if (!properties.TryGetValue(param.Name!, out var value))
                throw new InvalidOperationException(
                    $"Payload is missing '{param.Name}' for method '{method.Name}'.");

            args[i] = value.Deserialize(param.ParameterType, JsonOptions);
        }

        return args;
    }

    private static Dictionary<string, JsonElement> GetPayloadProperties(JsonElement? payload)
    {
        if (payload is null || payload.Value.ValueKind != JsonValueKind.Object)
            return new(StringComparer.OrdinalIgnoreCase);

        return payload.Value
            .EnumerateObject()
            .ToDictionary(p => p.Name, p => p.Value, StringComparer.OrdinalIgnoreCase);
    }
}