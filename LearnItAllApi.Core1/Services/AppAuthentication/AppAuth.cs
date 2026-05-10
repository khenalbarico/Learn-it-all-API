using System.Text;
using System.Text.Json;
using LearnItAllApi.DTO1.GetAuthUser;
using LearnItAllApi.Infrastructure1.FirebaseServices.Authentication;

namespace LearnItAllApi.Core1.Services.AppAuthentication;

public class AppAuth(IFirebaseAuth _firebaseAuth) : IAppAuth
{
    public async Task<GetSignInResult> SignInAsync(string email, string password)
    {
        var idToken = await _firebaseAuth.SignInAsync(email, password);
        var claims  = DecodeJwtPayload(idToken);

        return new GetSignInResult(
            UserId     : claims.GetProperty("user_id").GetString()!,
            Email      : email,
            DisplayName: claims.TryGetProperty("name", out var n) ? n.GetString() ?? "" : "",
            IdToken    : idToken);
    }

    public async Task SignUpAsync(string email, string password)
        => await _firebaseAuth.SignUpAsync(email, password);

    static JsonElement DecodeJwtPayload(string idToken)
    {
        var segment = idToken.Split('.')[1];
        var padded  = segment.PadRight(segment.Length + (4 - segment.Length % 4) % 4, '=');
        var json    = Encoding.UTF8.GetString(Convert.FromBase64String(padded));

        return JsonDocument.Parse(json).RootElement;
    }
}