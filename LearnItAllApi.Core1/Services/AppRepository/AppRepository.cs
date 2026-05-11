using LearnItAllApi.DTO1.Books;
using LearnItAllApi.DTO1.Users;
using LearnItAllApi.Infrastructure1.FirebaseServices.RealtimeDatabase;

namespace LearnItAllApi.Core1.Services.AppRepository;

public class AppRepository(IFirebaseRealtimeDb _db) : IAppRepository
{
    public async Task<AppUser?> TryGetAppUser(string idToken, string uid)
    {
        if (string.IsNullOrWhiteSpace(idToken))
            throw new UnauthorizedAccessException("ID token is required to access user data.");

        try
        {
            var user = await _db.GetAsync<AppUser>(idToken, "Users", uid);

            return string.IsNullOrWhiteSpace(user?.Uid) ? null : user;
        }
        catch
        {
            return null;
        }
    }

    public async Task SaveAppUser(string idToken, AppUser user)
    {
        if (string.IsNullOrWhiteSpace(idToken))
            throw new UnauthorizedAccessException("ID token is required to access user data.");
        try
        {
            await _db.PostAsync(user, idToken, "Users");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        try
        {
            return await _db.GetListAsync<Book>("Books", "CollegeCourses");
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

}