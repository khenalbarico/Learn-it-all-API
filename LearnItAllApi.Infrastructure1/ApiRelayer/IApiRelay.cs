using Microsoft.Azure.Functions.Worker.Http;

namespace LearnItAllApi.Infrastructure1.ApiRelayer;

public interface IApiRelay
{
    Task<HttpResponseData> GetResponse(
         HttpRequestData   req,
         CancellationToken ct = default);
}
