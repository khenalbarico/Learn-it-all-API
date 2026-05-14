namespace LearnItAllApi.Infrastructure1.FirebaseServices.FireStoreDatabase;

public interface IFirebaseFirestoreDb
{
    Task<T?> GetAsync<T>(params string[] pathSegments) where T : class;
    Task<T?> GetAuthAsync<T>(string idToken, params string[] pathSegments) where T : class;
    Task<List<T>> GetListAsync<T>(params string[] pathSegments) where T : class, new();
    Task<List<T>> GetAuthListAsync<T>(string idToken, params string[] pathSegments) where T : class, new();
    Task<string> PostAsync<T>(T item, params string[] pathSegments) where T : class;
    Task<string> PostAuthAsync<T>(T item, string idToken, params string[] pathSegments) where T : class;
    Task PutAsync<T>(T item, params string[] pathSegments) where T : class;
    Task PutAuthAsync<T>(T item, string idToken, params string[] pathSegments) where T : class;
    Task PatchAsync(Dictionary<string, object?> updates, params string[] pathSegments);
    Task PatchAuthAsync(Dictionary<string, object?> updates, string idToken, params string[] pathSegments);
    Task DeleteAsync(params string[] pathSegments);
    Task DeleteAuthAsync(string idToken, params string[] pathSegments);
}