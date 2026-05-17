using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;

namespace LearnItAllApi.Infrastructure1.FirebaseServices.RealtimeDatabase;

public class FirebaseRealtimeDb1(IFirebaseCfg _cfg) : IFirebaseRealtimeDb
{
    ChildQuery BuildQuery(params string[] childPaths)
    {
        var client = _cfg.CreateDbClient();
        var query  = client.Child(childPaths[0]);
        for (int i = 1; i < childPaths.Length; i++)
            query = query.Child(childPaths[i]);
        return query;
    }

    public async Task<T> GetAsync<T>(params string[] childPaths)
        => await BuildQuery(childPaths).OnceSingleAsync<T>();

    public async Task<List<T>> GetListAsync<T>(params string[] childPaths)
        where T : class, new()
    {
        var items = await BuildQuery(childPaths).OnceAsync<T>();
        return [.. items.Select(x =>
        {
            var value   = x.Object ?? new T();
            var uidProp = typeof(T).GetProperty("Uid");
            if (uidProp is not null && uidProp.CanWrite)
                uidProp.SetValue(value, x.Key);
            return value;
        })];
    }

    public async Task<FirebaseObject<T>> PostAsync<T>(T item, params string[] childPaths)
        => await BuildQuery(childPaths).PostAsync(item);

    public async Task PutAsync<T>(T item, params string[] childPaths)
        => await BuildQuery(childPaths).PutAsync(item);

    public async Task PatchNodeAsync<T>(T item, params string[] childPaths)
        => await BuildQuery(childPaths).PatchAsync(JsonConvert.SerializeObject(item));

    public async Task PatchFieldsAsync(Dictionary<string, object?> updates, params string[] childPaths)
    {
        if (updates is null || updates.Count == 0) return;
        await BuildQuery(childPaths).PatchAsync(JsonConvert.SerializeObject(updates));
    }

    public async Task DeleteAsync(params string[] childPaths)
        => await BuildQuery(childPaths).DeleteAsync();
}