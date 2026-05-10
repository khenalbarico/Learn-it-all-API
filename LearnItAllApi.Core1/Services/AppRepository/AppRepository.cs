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

        await _db.PostAsync(user, idToken, "Users");
    }
}