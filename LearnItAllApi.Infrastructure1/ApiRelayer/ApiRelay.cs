using Microsoft.Azure.Functions.Worker.Http;
using System.Net;
using System.Reflection;
using System.Text.Json;

namespace LearnItAllApi.Infrastructure1.ApiRelayer;

public sealed class ApiRelay(IRelayDispatcher _dispatcher) : IApiRelay
{
    static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    public async Task<HttpResponseData> GetResponse(HttpRequestData req, CancellationToken ct = default)
    {
        try
        {
            var relayReq = await JsonSerializer.DeserializeAsync<RelayReq>(req.Body, JsonOptions, ct)
                ?? throw new InvalidOperationException("Request body is required.");

            var result = await _dispatcher.DispatchAsync(relayReq, ct);
            var resp   = req.CreateResponse(HttpStatusCode.OK);

            await resp.WriteAsJsonAsync(result, ct);

            return resp;
        }
        catch (TargetInvocationException ex) when (ex.InnerException is not null)
        {
            return await req.CreateTextResponse(HttpStatusCode.InternalServerError, ex.InnerException.Message);
        }
        catch (Exception ex)
        {
            return await req.CreateTextResponse(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}