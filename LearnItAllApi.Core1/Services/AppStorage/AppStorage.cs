using LearnItAllApi.Core1.Services.AppRepository;
using LearnItAllApi.Infrastructure1.FirebaseServices.FirebaseErrors;
using LearnItAllApi.Infrastructure1.FirebaseServices.Storage;

namespace LearnItAllApi.Core1.Services.AppStorage;

public class AppStorage(
             IFirebaseStorage _storage,
             IAppRepository _repo) : IAppStorage
{
    public async Task<string> UploadAsync(Stream fileStream, string contentType, string idToken, params string[] pathSegments)
    {
        try
        {
            return await _storage.UploadAsync(fileStream, contentType, idToken, pathSegments);
        }
        catch (FirebaseStorageException)
        {
            throw;
        }
        catch (Exception ex)
        {
            FirebaseErrorHandler.ThrowStorage(ex);
            throw;
        }
    }

    public async Task<Stream> DownloadAsync(string idToken, params string[] pathSegments)
    {
        try
        {
            return await _storage.DownloadAsync(idToken, pathSegments);
        }
        catch (FirebaseStorageException)
        {
            throw;
        }
        catch (Exception ex)
        {
            FirebaseErrorHandler.ThrowStorage(ex);
            throw;
        }
    }

    public async Task<string> GetDownloadUrlAsync(string idToken, params string[] pathSegments)
    {
        try
        {
            return await _storage.GetDownloadUrlAsync(idToken, pathSegments);
        }
        catch (FirebaseStorageException)
        {
            throw;
        }
        catch (Exception ex)
        {
            FirebaseErrorHandler.ThrowStorage(ex);
            throw;
        }
    }

    public async Task DeleteAsync(string idToken, params string[] pathSegments)
    {
        try
        {
            await _storage.DeleteAsync(idToken, pathSegments);
        }
        catch (FirebaseStorageException)
        {
            throw;
        }
        catch (Exception ex)
        {
            FirebaseErrorHandler.ThrowStorage(ex);
            throw;
        }
    }

    public async Task<Stream> GetBookStreamAsync(string verifiedUid, string bookUid)
    {
        try
        {
            var owns = await _repo.UserOwnsBookAsync(verifiedUid, bookUid);
            if (!owns)
                throw new UnauthorizedAccessException($"User '{verifiedUid}' does not own book '{bookUid}'.");

            return await _storage.DownloadAsync(verifiedUid, "Books", bookUid, "book.pdf");
        }
        catch (UnauthorizedAccessException)
        {
            throw;
        }
        catch (FirebaseStorageException)
        {
            throw;
        }
        catch (Exception ex)
        {
            FirebaseErrorHandler.ThrowStorage(ex);
            throw;
        }
    }

    public async Task<string> GetCoverImageUrlAsync(string bookUid)
    {
        try
        {
            return await _storage.GetDownloadUrlAsync(string.Empty, "DisplayImages", bookUid, "cover.png");
        }
        catch (FirebaseStorageException)
        {
            throw;
        }
        catch (Exception ex)
        {
            FirebaseErrorHandler.ThrowStorage(ex);
            throw;
        }
    }
}
