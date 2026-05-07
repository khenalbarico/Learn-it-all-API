using LearnItAllApi.Infrastructure1.FirebaseServices.Authentication;
using TestProject1.TestTools;
using Xunit.Abstractions;

namespace TestProject1.FirebaseServicesFacts;

public class AuthenticationFacts1 (ITestOutputHelper _ctx)
{
    [Fact] public async Task Authentication_SignIn_User()
    {
        var payload = new
        {
            Email    = "khenalbarico05@gmail.com",
            Password = "Antimage05-",
        };

        var sut = _ctx.Get<IFirebaseAuth>();

        var res = await sut.SignInAsync(payload.Email, payload.Password);
    }

    [Fact] public async Task Authentication_SignUp_User()
    {
        var payload = new
        {
            Email    = "khenalbarico05@gmail.com",
            Password = "Antimage05-",
        };

        var sut = _ctx.Get<IFirebaseAuth>();

        await sut.SignUpAsync(payload.Email, payload.Password);
    }
}
