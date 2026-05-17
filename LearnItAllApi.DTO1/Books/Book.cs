using System.ComponentModel.DataAnnotations;

namespace LearnItAllApi.DTO1.Books;

public class Book
{
    [Required] public string         Uid         { get; set; } = "";
               public string         Category    { get; set; } = "";
               public string         ImageUrl    { get; set; } = "";
               public string         Title       { get; set; } = "";
               public string         Description { get; set; } = "";
               public decimal        Price       { get; set; }
}
