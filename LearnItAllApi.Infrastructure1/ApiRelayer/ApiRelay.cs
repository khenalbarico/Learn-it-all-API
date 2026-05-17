using Microsoft.Azure.Functions.Worker.Http;
using System.Net;
using System.Reflection;
using System.Text.Json;

namespace LearnItAllApi.Infrastructure1.ApiRelayer;

public sealed class ApiRelay(IRelayDispatcher _dispatcher, IFirebaseTokenVerifier _tokenVerifier) : IApiRelay
{
    static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    public async Task<HttpResponseData> GetResponse(HttpRequestData req, CancellationToken ct = default)
    {
        try
        {
            if (!req.Headers.TryGetValues("Authorization", out var authValues))
                return await req.CreateTextResponse(HttpStatusCode.Unauthorized, "Missing authorization header.");

            var raw   = authValues.FirstOrDefault() ?? string.Empty;
            var token = raw.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)
                ? raw["Bearer ".Length..].Trim()
                : raw.Trim();

            var verifiedUid = await _tokenVerifier.VerifyAsync(token, ct);

            if (verifiedUid is null)
                return await req.CreateTextResponse(HttpStatusCode.Unauthorized, "Invalid or expired token.");

            var relayReq = await JsonSerializer.DeserializeAsync<RelayReq>(req.Body, JsonOptions, ct)
                ?? throw new InvalidOperationException("Request body is required.");

            relayReq.VerifiedUid = verifiedUid;

            var result = await _dispatcher.DispatchAsync(relayReq, ct);
            var resp   = req.CreateResponse(HttpStatusCode.OK);

            await resp.WriteAsJsonAsync(result, ct);

            return resp;
        }
        catch (TargetInvocationException ex) when (ex.InnerException is not null)
        {
            return await req.CreateTextResponse(HttpStatusCode.InternalServerError, ex.InnerException.Message);
        }
        catch (UnauthorizedAccessException ex)
        {
            return await req.CreateTextResponse(HttpStatusCode.Forbidden, ex.Message);
        }
        catch (Exception ex)
        {
            return await req.CreateTextResponse(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}