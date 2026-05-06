using System.Text.Json;

namespace LearnItAllApi.Infrastructure1.ApiRelayer;

public sealed class RelayReq
{
    public string       ClassName  { get; init; } = string.Empty;
    public string       MethodName { get; init; } = string.Empty;
    public JsonElement? Payload    { get; init; }
}