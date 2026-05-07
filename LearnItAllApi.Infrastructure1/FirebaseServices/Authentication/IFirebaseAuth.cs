using Firebase.Auth;

namespace LearnItAllApi.Infrastructure1.FirebaseServices.Authentication;

public interface IFirebaseAuth
{
    Task<string> SignInAsync(
        string email,
        string password);
    Task SignUpAsync(
        string email,
        string password);
}