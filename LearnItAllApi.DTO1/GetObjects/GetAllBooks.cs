using LearnItAllApi.DTO1.Books;

namespace LearnItAllApi.DTO1.GetObjects;

public class GetAllBooks
{
    public IEnumerable<Book> Books       { get; set; } = [];
}
