using Firebase.Auth;
using System.Collections.Concurrent;

namespace LearnItAllApi.Infrastructure1.FirebaseServices;

public class FirebaseSessionStore : IFirebaseSessionStore
{
    readonly ConcurrentDictionary<string, FirebaseAuthClient> _sessions = new();

    public void Store(string userId, FirebaseAuthClient client)
        => _sessions[userId] = client;

    public bool TryGet(string userId, out FirebaseAuthClient? client)
        => _sessions.TryGetValue(userId, out client);

    public void Remove(string userId)
        => _sessions.TryRemove(userId, out _);
}