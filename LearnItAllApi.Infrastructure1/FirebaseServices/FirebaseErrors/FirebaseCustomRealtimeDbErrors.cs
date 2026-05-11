namespace LearnItAllApi.Infrastructure1.FirebaseServices.FirebaseErrors;

public static class FirebaseCustomRealtimeDbErrors
{
    static readonly Dictionary<string, string> _map = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Permission denied"]         = "You do not have permission to access this data.",
        ["PERMISSION_DENIED"]    = "You do not have permission to access this data.",
        ["UNAUTHORIZED"]            = "Authentication is required to access this data.",
        ["invalid_token"]                  = "Your session has expired. Please sign in again.",
        ["token_expired"]                = "Your session has expired. Please sign in again.",
    };

    public static string Translate(string? firebaseCode) =>
        firebaseCode is not null && _map.TryGetValue(firebaseCode, out var msg)
            ? msg
            : "A database error occurred. Please try again.";
}
