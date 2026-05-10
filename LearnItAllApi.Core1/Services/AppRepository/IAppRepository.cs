using LearnItAllApi.DTO1.Users;

namespace LearnItAllApi.Core1.Services.AppRepository;

public interface IAppRepository
{
    Task<AppUser?> TryGetAppUser(string idToken, string uid);
}
