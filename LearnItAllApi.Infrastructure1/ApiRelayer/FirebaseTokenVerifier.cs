namespace LearnItAllApi.Infrastructure1.ApiRelayer;

public sealed class FirebaseTokenVerifier : IFirebaseTokenVerifier
{
    public async Task<string?> VerifyAsync(string? idToken, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(idToken)) return null;
        try
        {
            var decoded = await FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken, ct);
            return decoded.Uid;
        }
        catch
        {
            return null;
        }
    }
}