namespace LearnItAllApi.DTO1.GetObjects;

public class GetAllResponseCollection
{
    public IEnumerable<Book> Books       { get; set; } = [];
    public User              CurrentUser { get; set; } = new User();
}
