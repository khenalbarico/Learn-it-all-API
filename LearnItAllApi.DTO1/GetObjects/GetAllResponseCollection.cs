using LearnItAllApi.DTO1.Books;
using LearnItAllApi.DTO1.Users;

namespace LearnItAllApi.DTO1.GetObjects;

public class GetAllResponseCollection
{
    public IEnumerable<Book> Books       { get; set; } = [];
    public AppUser              CurrentUser { get; set; } = new AppUser();
}
