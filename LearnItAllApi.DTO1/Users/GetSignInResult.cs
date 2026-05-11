namespace LearnItAllApi.DTO1.Users;

public record GetSignInResult(
    string UserId,
    string Email,
    string DisplayName,
    string IdToken);
