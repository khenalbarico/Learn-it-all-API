namespace LearnItAllApi.Core1.Services.AppStorage;

public interface IAppStorage
{
    Task<string> UploadAsync(Stream fileStream, string contentType, string idToken, params string[] pathSegments);
    Task<Stream> DownloadAsync(string idToken, params string[] pathSegments);
    Task<string> GetDownloadUrlAsync(string idToken, params string[] pathSegments);
    Task DeleteAsync(string idToken, params string[] pathSegments);
}
