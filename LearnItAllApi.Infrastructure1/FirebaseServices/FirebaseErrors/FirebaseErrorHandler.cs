using System.Text.Json;

namespace LearnItAllApi.Infrastructure1.FirebaseServices.FirebaseErrors;

public static class FirebaseErrorHandler
{
    static string? ExtractFirebaseCode(string rawMessage)
    {
        int start = rawMessage.IndexOf('{');
        if (start < 0) return null;

        int end = rawMessage.LastIndexOf('}');
        if (end <= start) return null;

        try
        {
            var json = rawMessage[start..(end + 1)];
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            if (root.TryGetProperty("error", out var err) &&
                err.TryGetProperty("message", out var msg))
                return msg.GetString();

            if (root.TryGetProperty("message", out var flat))
                return flat.GetString();
        }
        catch (JsonException) { }

        return null;
    }

    public static void ThrowAuth(Exception ex)
    {
        var code = ExtractFirebaseCode(ex.Message);
        throw new FirebaseAuthException(FirebaseCustomAuthErrors.Translate(code));
    }

    public static void ThrowRealtimeDb(Exception ex)
    {
        var code = ExtractFirebaseCode(ex.Message);
        throw new FirebaseRealtimeDbException(FirebaseCustomRealtimeDbErrors.Translate(code));
    }

    public static void ThrowStorage(Exception ex)
    {
        var code = ExtractFirebaseCode(ex.Message);
        throw new FirebaseStorageException(FirebaseCustomStorageErrors.Translate(code));
    }
}
