using Firebase.Database;

namespace LearnItAllApi.Infrastructure1.FirebaseServices.RealtimeDatabase;

public interface IFirebaseRealtimeDb
{
    Task<T> GetAsync<T>(params string[] childPaths);

    Task<T> GetAsync<T>(
        string userId,
        params string[] childPaths);

    Task<List<T>> GetListAsync<T>(
        string userId,
        params string[] childPaths) where T : class, new();

    Task<FirebaseObject<T>> PostAsync<T>(
        T item,
        string userId,
        params string[] childPaths);

    Task PutAsync<T>(
        T item,
        string userId,
        params string[] childPaths);

    Task PatchNodeAsync<T>(
        T item,
        string userId,
        params string[] childPaths);

    Task PatchFieldsAsync(
        Dictionary<string, object?> updates,
        string userId,
        params string[] childPaths);

    Task DeleteAsync(
        string userId,
        params string[] childPaths);
}