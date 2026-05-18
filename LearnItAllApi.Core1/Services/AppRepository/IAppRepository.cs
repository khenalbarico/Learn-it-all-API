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
    Task<IEnumerable<Book>> GetAllBooksAsync(string category);

    [RelayAuthorize]
    Task<Book?> GetBookAsync(string category, string bookUid);

    [RelayAuthorize]
    Task AddToLibraryAsync(string verifiedUid, LibraryEntry entry);

    [RelayAuthorize]
    Task<bool> UserOwnsBookAsync(string verifiedUid, string bookUid);
}
