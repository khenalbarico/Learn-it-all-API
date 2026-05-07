using System.ComponentModel.DataAnnotations;

namespace LearnItAllApi.DTO1;

public class User
{
    [Required] public string Uid         { get; set; } = "";
               public string BearerToken { get; set; } = "";
}
