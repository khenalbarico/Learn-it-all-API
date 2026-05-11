namespace LearnItAllApi.Infrastructure1.FirebaseServices.FirebaseErrors;

public static class FirebaseCustomAuthErrors
{
    static readonly Dictionary<string, string> _map = new(StringComparer.OrdinalIgnoreCase)
    {
        // Sign-in errors
        ["INVALID_PASSWORD"]              = "The password is incorrect. Please try again.",
        ["INVALID_LOGIN_CREDENTIALS"]     = "Invalid email or password.",
        ["EMAIL_NOT_FOUND"]               = "No account found with that email address.",
        ["USER_DISABLED"]                 = "This account has been disabled.",
        ["USER_NOT_FOUND"]                = "No account found with that email address.",
        ["TOO_MANY_ATTEMPTS_TRY_LATER"]   = "Too many failed attempts. Please try again later.",

        // Sign-up errors
        ["EMAIL_EXISTS"]                  = "An account with this email already exists.",
        ["OPERATION_NOT_ALLOWED"]         = "Email/password sign-in is not enabled.",
        ["WEAK_PASSWORD"]                 = "Password must be at least 6 characters.",
        ["INVALID_EMAIL"]                 = "The email address is not valid.",

        // Token / session errors
        ["TOKEN_EXPIRED"]                 = "Your session has expired. Please sign in again.",
        ["INVALID_ID_TOKEN"]              = "Invalid session token. Please sign in again.",
        ["CREDENTIAL_TOO_OLD_LOGIN_AGAIN"]= "Please sign in again to continue.",

        // Password reset
        ["EXPIRED_OOB_CODE"]              = "This reset link has expired. Please request a new one.",
        ["INVALID_OOB_CODE"]              = "This reset link is invalid or has already been used.",
    };

    public static string Translate(string? firebaseCode) =>
        firebaseCode is not null && _map.TryGetValue(firebaseCode, out var msg)
            ? msg
            : "An authentication error occurred. Please try again.";
}
