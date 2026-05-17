using System.Text.Json;

namespace LearnItAllApi.Infrastructure1.ApiRelayer;

public sealed class RelayReq
{
    public string       ClassName   { get; set; } = string.Empty;
    public string       MethodName  { get; set; } = string.Empty;
    public JsonElement? Payload     { get; set; }
    public string?      VerifiedUid { get; set; }
}