using Google.Cloud.Firestore;
using LearnItAllApi.Infrastructure1.FirebaseServices.FireStoreDatabase;

namespace LearnItAllApi.Infrastructure1.FirebaseServices.FirestoreDatabase;

public class FirebaseFirestoreDb(IFirebaseCfg _cfg) : IFirebaseFirestoreDb
{
    CollectionReference BuildCollectionRef(FirestoreDb db, string[] pathSegments)
    {
        if (pathSegments.Length < 1) throw new ArgumentException("At least one path segment required.");

        DocumentReference? docRef = null;
        CollectionReference? colRef = null;

        for (int i = 0; i < pathSegments.Length; i++)
        {
            if (i % 2 == 0)
                colRef = docRef is null ? db.Collection(pathSegments[i]) : docRef.Collection(pathSegments[i]);
            else
                docRef = colRef!.Document(pathSegments[i]);
        }

        if (colRef is null) throw new ArgumentException("Path must end at a collection.");
        return colRef;
    }

    DocumentReference BuildDocumentRef(FirestoreDb db, string[] pathSegments)
    {
        if (pathSegments.Length < 2) throw new ArgumentException("At least two path segments required for a document.");

        DocumentReference? docRef = null;
        CollectionReference? colRef = null;

        for (int i = 0; i < pathSegments.Length; i++)
        {
            if (i % 2 == 0)
                colRef = docRef is null ? db.Collection(pathSegments[i]) : docRef.Collection(pathSegments[i]);
            else
                docRef = colRef!.Document(pathSegments[i]);
        }

        if (docRef is null) throw new ArgumentException("Path must end at a document.");
        return docRef;
    }

    public async Task<T?> GetAsync<T>(params string[] pathSegments) where T : class
    {
        var db = _cfg.CreateFirestoreClient();
        var docRef = BuildDocumentRef(db, pathSegments);
        var snapshot = await docRef.GetSnapshotAsync();

        if (!snapshot.Exists) return null;
        var result = snapshot.ConvertTo<T>();
        SetDocumentId(result, snapshot.Id);
        return result;
    }

    public async Task<List<T>> GetListAsync<T>(params string[] pathSegments) where T : class, new()
    {
        var db = _cfg.CreateFirestoreClient();
        var colRef = BuildCollectionRef(db, pathSegments);
        var snapshots = await colRef.GetSnapshotAsync();

        return [.. snapshots.Documents.Select(doc =>
        {
            var item = doc.ConvertTo<T>() ?? new T();
            SetDocumentId(item, doc.Id);
            return item;
        })];
    }

    public async Task<List<T>> GetListWithFilterAsync<T>(
    string field, object value, params string[] pathSegments) where T : class, new()
    {
        var db        = _cfg.CreateFirestoreClient();
        var colRef    = BuildCollectionRef(db, pathSegments);
        var query     = colRef.WhereEqualTo(field, value);
        var snapshots = await query.GetSnapshotAsync();
        return [.. snapshots.Documents.Select(doc =>
    {
        var item = doc.ConvertTo<T>() ?? new T();
        SetDocumentId(item, doc.Id);
        return item;
    })];
    }

    public async Task<string> PostAsync<T>(T item, params string[] pathSegments) where T : class
    {
        var db = _cfg.CreateFirestoreClient();
        var colRef = BuildCollectionRef(db, pathSegments);
        var docRef = await colRef.AddAsync(item);
        return docRef.Id;
    }

    public async Task PutAsync<T>(T item, params string[] pathSegments) where T : class
    {
        var db = _cfg.CreateFirestoreClient();
        var docRef = BuildDocumentRef(db, pathSegments);
        await docRef.SetAsync(item);
    }

    public async Task PatchAsync(Dictionary<string, object?> updates, params string[] pathSegments)
    {
        if (updates is null || updates.Count == 0) return;

        var db = _cfg.CreateFirestoreClient();
        var docRef = BuildDocumentRef(db, pathSegments);
        await docRef.UpdateAsync(updates!);
    }

    public async Task DeleteAsync(params string[] pathSegments)
    {
        var db = _cfg.CreateFirestoreClient();
        var docRef = BuildDocumentRef(db, pathSegments);
        await docRef.DeleteAsync();
    }

    static void SetDocumentId<T>(T item, string id)
    {
        var prop = typeof(T).GetProperty("Id") ?? typeof(T).GetProperty("Uid");
        if (prop is not null && prop.CanWrite)
            prop.SetValue(item, id);
    }
}