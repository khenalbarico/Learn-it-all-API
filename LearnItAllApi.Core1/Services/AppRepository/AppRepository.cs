using LearnItAllApi.DTO1.Books;
using LearnItAllApi.DTO1.Users;
using LearnItAllApi.Infrastructure1.FirebaseServices.FirebaseErrors;
using LearnItAllApi.Infrastructure1.FirebaseServices.RealtimeDatabase;

namespace LearnItAllApi.Core1.Services.AppRepository;

public class AppRepository(IFirebaseRealtimeDb _db) : IAppRepository
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

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        try
        {
            return await _db.GetListAsync<Book>("Books", "CollegeCourses");
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
}