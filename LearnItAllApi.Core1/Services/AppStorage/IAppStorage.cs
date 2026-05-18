using LearnItAllApi.Infrastructure1.ApiRelayer;

namespace LearnItAllApi.Core1.Services.AppStorage;

public interface IAppStorage
{
    [RelayAuthorize]
    Task<string> UploadAsync(Stream fileStream, string contentType, string idToken, params string[] pathSegments);

    [RelayAuthorize]
    Task<Stream> DownloadAsync(string idToken, params string[] pathSegments);

    [RelayAuthorize]
    Task<string> GetDownloadUrlAsync(string idToken, params string[] pathSegments);

    [RelayAuthorize]
    Task DeleteAsync(string idToken, params string[] pathSegments);

    [RelayAuthorize]
    Task<Stream> GetBookStreamAsync(string verifiedUid, string bookUid);

    [RelayAuthorize]
    Task<string> GetCoverImageUrlAsync(string bookUid);
}
