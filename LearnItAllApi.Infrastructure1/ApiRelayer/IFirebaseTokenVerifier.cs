namespace LearnItAllApi.Infrastructure1.ApiRelayer;

public interface IFirebaseTokenVerifier
{
    Task<string?> VerifyAsync(string? idToken, CancellationToken ct);
}