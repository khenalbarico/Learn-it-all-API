using System.ComponentModel.DataAnnotations;
using LearnItAllApi.DTO1.Books;

namespace LearnItAllApi.DTO1.Users;

public class AppUser
{
    [Required] public string                           Uid          { get; set; } = "";
               public string                           Email        { get; set; } = "";
               public string                           DisplayName  { get; set; } = "";
               public SubscriptionType                 Subscription { get; set; } = SubscriptionType.Free;
               public Dictionary<string, LibraryEntry> Library      { get; set; } = [];
}
