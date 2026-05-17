using LearnItAllApi.DTO1.Books;
using LearnItAllApi.DTO1.Users;
using LearnItAllApi.Infrastructure1.ApiRelayer;

namespace LearnItAllApi.Core1.Services.AppRepository;

public interface IAppRepository
{
    [RelayAuthorize]
    Task<AppUser?> TryGetAppUser(string verifiedUid);

    [RelayAuthorize]
    Task SaveAppUser(string verifiedUid, AppUser user);

    [RelayAuthorize]
    Task<IEnumerable<Book>> GetAllBooksAsync();
}