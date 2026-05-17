using LearnItAllApi.DTO1.Users;
using LearnItAllApi.Infrastructure1.ApiRelayer;

namespace LearnItAllApi.Core1.Services.AppAuthentication;

public interface IAppAuth
{
    [RelayAuthorize(AllowAnonymous = true)]
    Task<GetSignInResult> SignInAsync(string email, string password);

    [RelayAuthorize(AllowAnonymous = true)]
    Task SignUpAsync(string email, string password);
}