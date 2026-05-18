using LearnItAllApi.DTO1.Books;
using LearnItAllApi.DTO1.Users;
using LearnItAllApi.Infrastructure1.FirebaseServices.FirebaseErrors;
using LearnItAllApi.Infrastructure1.FirebaseServices.FireStoreDatabase;
using LearnItAllApi.Infrastructure1.FirebaseServices.RealtimeDatabase;

namespace LearnItAllApi.Core1.Services.AppRepository;

public class AppRepository(
             IFirebaseRealtimeDb  _db,
             IFirebaseFirestoreDb _firestore) : IAppRepository
{
    public async Task<AppUser?> TryGetAppUser(string verifiedUid)
    {
        try
        {
            var user = await _db.GetAsync<AppUser>("Users", verifiedUid);
            return string.IsNullOrWhiteSpace(user?.Uid) ? null : user;
        }
        catch
        {
            return null;
        }
    }

    public async Task SaveAppUser(string verifiedUid, AppUser user)
    {
        try
        {
            user.Uid = verifiedUid;
            await _db.PutAsync(user, "Users", verifiedUid);
        }
        catch (FirebaseRealtimeDbException)
        {
            throw;
        }
        catch (Exception ex)
        {
            FirebaseErrorHandler.ThrowRealtimeDb(ex);
            throw;
        }
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync(string category)
    {
        try
        {
            return await _firestore.GetListAsync<Book>("Books", category);
        }
        catch (FirebaseFirestoreDbException)
        {
            throw;
        }
        catch (Exception ex)
        {
            FirebaseErrorHandler.ThrowFirestore(ex);
            throw;
        }
    }

    public async Task<Book?> GetBookAsync(string category, string bookUid)
    {
        try
        {
            return await _firestore.GetAsync<Book>("Books", category, bookUid);
        }
        catch (FirebaseFirestoreDbException)
        {
            throw;
        }
        catch (Exception ex)
        {
            FirebaseErrorHandler.ThrowFirestore(ex);
            throw;
        }
    }

    public async Task AddToLibraryAsync(string verifiedUid, LibraryEntry entry)
    {
        try
        {
            var user = await TryGetAppUser(verifiedUid)
                ?? throw new InvalidOperationException("User not found.");

            user.Library[entry.BookUid] = entry;
            await _db.PutAsync(user, "Users", verifiedUid);
        }
        catch (FirebaseRealtimeDbException)
        {
            throw;
        }
        catch (Exception ex)
        {
            FirebaseErrorHandler.ThrowRealtimeDb(ex);
            throw;
        }
    }

    public async Task<bool> UserOwnsBookAsync(string verifiedUid, string bookUid)
    {
        try
        {
            var user = await TryGetAppUser(verifiedUid);
            return user?.Library.ContainsKey(bookUid) ?? false;
        }
        catch
        {
            return false;
        }
    }
}
