namespace LearnItAllApi.DTO1.GetAuthUser;

public record GetSignInResult(
    string UserId,
    string Email,
    string DisplayName,
    string IdToken);
