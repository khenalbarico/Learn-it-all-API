using Firebase.Auth;

namespace LearnItAllApi.Infrastructure1.FirebaseServices;

public interface IFirebaseSessionStore
{
    void Store(string userId, FirebaseAuthClient client);
    bool TryGet(string userId, out FirebaseAuthClient? client);
    void Remove(string userId);
}