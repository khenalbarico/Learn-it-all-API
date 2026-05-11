using System.Text;
using System.Text.Json;
using LearnItAllApi.DTO1.Users;
using LearnItAllApi.Infrastructure1.FirebaseServices.Authentication;

namespace LearnItAllApi.Core1.Services.AppAuthentication;

public class AppAuth(IFirebaseAuth _firebaseAuth) : IAppAuth
{
    public async Task<GetSignInResult> SignInAsync(string email, string password)
    {
        try
        {
            var idToken = await _firebaseAuth.SignInAsync(email, password);
            var claims  = DecodeJwtPayload(idToken);

            return new GetSignInResult(
                UserId     : claims.GetProperty("user_id").GetString()!,
                Email      : email,
                DisplayName: claims.TryGetProperty("name", out var n) ? n.GetString() ?? "" : "",
                IdToken    : idToken);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }    
    }

    public async Task SignUpAsync(string email, string password)
    {
        try
        {
            await _firebaseAuth.SignUpAsync(email, password);
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    static JsonElement DecodeJwtPayload(string idToken)
    {
        var segment = idToken.Split('.')[1];
        var padded  = segment.PadRight(segment.Length + (4 - segment.Length % 4) % 4, '=');
        var json    = Encoding.UTF8.GetString(Convert.FromBase64String(padded));

        return JsonDocument.Parse(json).RootElement;
    }
}