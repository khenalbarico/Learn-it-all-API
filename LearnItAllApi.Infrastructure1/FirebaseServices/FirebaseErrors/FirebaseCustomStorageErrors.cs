namespace LearnItAllApi.Infrastructure1.FirebaseServices.FirebaseErrors;

public static class FirebaseCustomStorageErrors
{
    static readonly Dictionary<string, string> _map = new(StringComparer.OrdinalIgnoreCase)
    {
        ["unauthorized"]                  = "You are not authorised to access this file.",
        ["forbidden"]                     = "Access to this file is forbidden.",
        ["object-not-found"]              = "The requested file was not found.",
        ["bucket-not-found"]              = "The storage bucket was not found.",
        ["quota-exceeded"]                = "Storage quota exceeded.",
        ["unauthenticated"]               = "Authentication is required to access this file.",
        ["retry-limit-exceeded"]          = "The upload failed after too many retries. Please try again.",
        ["invalid-checksum"]              = "File upload failed due to a checksum mismatch. Please try again.",
        ["canceled"]                      = "The file operation was cancelled.",
    };

    public static string Translate(string? firebaseCode) =>
        firebaseCode is not null && _map.TryGetValue(firebaseCode, out var msg)
            ? msg
            : "A storage error occurred. Please try again.";
}
