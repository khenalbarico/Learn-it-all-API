using LearnItAllApi.DTO1.Users;

namespace LearnItAllApi.Core1.Services.AppAuthentication;

public interface IAppAuth
{
    Task<GetSignInResult> SignInAsync(string email, string password);
    Task SignUpAsync(string email, string password);
}
