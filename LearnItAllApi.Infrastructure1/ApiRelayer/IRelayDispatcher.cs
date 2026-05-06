namespace LearnItAllApi.Infrastructure1.ApiRelayer;

public interface IRelayDispatcher
{
    Task<object?> DispatchAsync(RelayReq req, CancellationToken ct = default);
}
