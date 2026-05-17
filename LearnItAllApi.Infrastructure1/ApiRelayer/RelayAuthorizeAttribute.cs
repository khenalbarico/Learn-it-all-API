namespace LearnItAllApi.Infrastructure1.ApiRelayer;

[AttributeUsage(AttributeTargets.Method)]
public sealed class RelayAuthorizeAttribute : Attribute
{
    public bool AllowAnonymous { get; init; } = false;
}