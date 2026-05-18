namespace LearnItAllApi.Infrastructure1.FirebaseServices.FirebaseErrors;

public static class FirebaseCustomFirestoreErrors
{
    static readonly Dictionary<string, string> _map = new(StringComparer.OrdinalIgnoreCase)
    {
        ["PERMISSION_DENIED"]       = "You do not have permission to access this data.",
        ["UNAUTHENTICATED"]         = "Authentication is required to access this data.",
        ["NOT_FOUND"]               = "The requested document does not exist.",
        ["ALREADY_EXISTS"]          = "A document with this ID already exists.",
        ["RESOURCE_EXHAUSTED"]      = "Firestore quota exceeded. Please try again later.",
        ["UNAVAILABLE"]             = "Firestore is temporarily unavailable. Please try again.",
        ["DEADLINE_EXCEEDED"]       = "The request timed out. Please try again.",
        ["INVALID_ARGUMENT"]        = "Invalid data was provided to Firestore.",
        ["FAILED_PRECONDITION"]     = "The operation was rejected. Please check your data and try again.",
        ["ABORTED"]                 = "The operation was aborted due to a conflict. Please retry.",
        ["INTERNAL"]                = "An internal Firestore error occurred. Please try again.",
    };

    public static string Translate(string? firestoreCode) =>
        firestoreCode is not null && _map.TryGetValue(firestoreCode, out var msg)
            ? msg
            : "A database error occurred. Please try again.";
}
