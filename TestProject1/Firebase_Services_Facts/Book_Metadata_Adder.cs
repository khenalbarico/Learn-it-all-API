using LearnItAllApi.Core1.Services.AppRepository;
using LearnItAllApi.DTO1.Books;
using TestProject1.TestTools;
using Xunit.Abstractions;

namespace TestProject1.Firebase_Services_Facts;

public class Book_Metadata_Adder (ITestOutputHelper _ctx)
{
    [Fact]
    public async Task Add_CollegeCourse_Book_Metadata()
    {
        var prefix   = "IS"; 

        var metadata = new Book
        {
            Uid         = prefix.GenerateUid(),
            Category    = "CollegeCourse",
            Title       = "Information System",
            Description = "Complete Information System learning materials for students and aspiring developers. Covers programming, algorithms, data management, front-end development, OOP, and more. Includes lifetime reading . Perfect for review, self-study, and academic learning.",
            Price       = 70
        };

        var _sut = _ctx.Get<IAppRepository>();

        await _sut.AddBookMetadata(metadata);
    }
}
