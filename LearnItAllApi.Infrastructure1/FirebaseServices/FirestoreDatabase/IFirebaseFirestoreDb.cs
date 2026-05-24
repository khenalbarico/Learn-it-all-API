namespace LearnItAllApi.Infrastructure1.FirebaseServices.FireStoreDatabase;

public interface IFirebaseFirestoreDb
{
    Task<T?> GetAsync<T>(params string[] pathSegments) where T : class;
    Task<List<T>> GetListAsync<T>(params string[] pathSegments) where T : class, new();
    Task<List<T>> GetListWithFilterAsync<T>(
    string field, object value, params string[] pathSegments) where T : class, new();
    Task<string> PostAsync<T>(T item, params string[] pathSegments) where T : class;
    Task PutAsync<T>(T item, params string[] pathSegments) where T : class;
    Task PatchAsync(Dictionary<string, object?> updates, params string[] pathSegments);
    Task DeleteAsync(params string[] pathSegments);
}