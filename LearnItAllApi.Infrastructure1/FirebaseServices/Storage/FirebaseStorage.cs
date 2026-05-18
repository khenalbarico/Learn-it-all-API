using System.Net.Http.Headers;

namespace LearnItAllApi.Infrastructure1.FirebaseServices.Storage;

public class FirebaseStorage(IFirebaseCfg _cfg, HttpClient _httpClient) : IFirebaseStorage
{
    string BuildUrl(params string[] pathSegments)
    {
        var encoded = pathSegments.Select(Uri.EscapeDataString);
        var path = string.Join("%2F", encoded);
        return $"https://firebasestorage.googleapis.com/v0/b/{_cfg.StorageBucket}/o/{path}";
    }

    HttpRequestMessage BuildRequest(HttpMethod method, string url, string idToken, HttpContent? content = null)
    {
        var req = new HttpRequestMessage(method, url);
        if (!string.IsNullOrWhiteSpace(idToken))
            req.Headers.Authorization = new AuthenticationHeaderValue("Firebase", idToken);
        if (content is not null) req.Content = content;
        return req;
    }

    public async Task<string> UploadAsync(Stream fileStream, string contentType, string idToken, params string[] pathSegments)
    {
        var url = BuildUrl(pathSegments);
        var content = new StreamContent(fileStream);
        content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
        using var req = BuildRequest(HttpMethod.Post, url, idToken, content);
        using var res = await _httpClient.SendAsync(req);
        var resTxt = await res.Content.ReadAsStringAsync();
        if (!res.IsSuccessStatusCode)
            throw new InvalidOperationException($"Upload failed: {resTxt}");
        return $"{url}?alt=media";
    }

    public async Task<Stream> DownloadAsync(string idToken, params string[] pathSegments)
    {
        var url = $"{BuildUrl(pathSegments)}?alt=media";
        using var req = BuildRequest(HttpMethod.Get, url, idToken);
        var res = await _httpClient.SendAsync(req, HttpCompletionOption.ResponseHeadersRead);
        if (!res.IsSuccessStatusCode)
            throw new InvalidOperationException($"Download failed: {await res.Content.ReadAsStringAsync()}");
        return await res.Content.ReadAsStreamAsync();
    }

    public async Task<string> GetDownloadUrlAsync(string idToken, params string[] pathSegments)
    {
        var url = $"{BuildUrl(pathSegments)}?alt=media";
        using var req = BuildRequest(HttpMethod.Get, url, idToken);
        using var res = await _httpClient.SendAsync(req, HttpCompletionOption.ResponseHeadersRead);
        if (!res.IsSuccessStatusCode)
            throw new InvalidOperationException($"Failed to get download URL: {await res.Content.ReadAsStringAsync()}");
        return url;
    }

    public async Task DeleteAsync(string idToken, params string[] pathSegments)
    {
        var url = BuildUrl(pathSegments);
        using var req = BuildRequest(HttpMethod.Delete, url, idToken);
        using var res = await _httpClient.SendAsync(req);
        if (!res.IsSuccessStatusCode)
            throw new InvalidOperationException($"Delete failed: {await res.Content.ReadAsStringAsync()}");
    }
}
