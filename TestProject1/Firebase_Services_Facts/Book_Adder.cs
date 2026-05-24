using LearnItAllApi.Core1.Services.AppAuthentication;
using LearnItAllApi.Core1.Services.AppRepository;
using LearnItAllApi.Core1.Services.AppStorage;
using LearnItAllApi.DTO1.Books;
using TestProject1.TestTools;
using TestProject1.TestTools.Book_Adder_Utils;
using Xunit.Abstractions;

namespace TestProject1.Firebase_Services_Facts;

public class Book_Adder(ITestOutputHelper _ctx)
{
    const string TestEmail    = "khenalbarico05@gmail.com";
    const string TestPassword = "Antimage05-";

    [Fact]
    public async Task Seed_All_Books()
    {
        var idToken = (await _ctx.Get<IAppAuth>().SignInAsync(TestEmail, TestPassword)).IdToken;
        var repo    = _ctx.Get<IAppRepository>();
        var storage = _ctx.Get<IAppStorage>();

        foreach (var seed in BookSeeds.All.Where(s => !s.Skip))
            await UploadBookAsync(repo, storage, idToken, seed);
    }

    static async Task UploadBookAsync(IAppRepository repo, IAppStorage storage, string idToken, BookSeed seed)
    {
        var uid            = seed.Prefix.GenerateUid();
        var bookFolderPath = Path.Combine(AppContext.BaseDirectory, $"TestFiles/{seed.Category}/{seed.Title}");

        using var cover = TestHelpers.GetFileStream(
            Path.Combine(AppContext.BaseDirectory, $"TestFiles/{seed.Category}/{seed.Title}/{seed.CoverFileName}"));
        var coverUrl = await storage.UploadAsync(cover, "image/png", idToken,
                           "CoverImages", seed.Category, uid, seed.CoverFileName);

        await repo.AddBookMetadata(new Book
        {
            Uid         = uid,
            Category    = seed.Category,
            Title       = seed.Title,
            Description = seed.Description,
            Price       = seed.Price,
            CoverUrl    = coverUrl,
        });

        //foreach (var filePath in TestHelpers.GetFilePaths($"TestFiles/{seed.Category}/{seed.Title}/")
        //                                    .Where(p => !p.EndsWith(seed.CoverFileName, StringComparison.OrdinalIgnoreCase)))
        //{
        //    var relativePath = filePath[(bookFolderPath.Length + 1)..];
        //    using var stream = TestHelpers.GetFileStream(filePath);
        //    await storage.UploadAsync(stream, ContentType(filePath), idToken, "Books", seed.Category, uid, relativePath);
        //}
    }

    static string ContentType(string path) => Path.GetExtension(path).ToLowerInvariant() switch
    {
        ".pdf"  => "application/pdf",
        ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
        ".doc"  => "application/msword",
        ".png"  => "image/png",
        ".jpg" or ".jpeg" => "image/jpeg",
        _       => "application/octet-stream",
    };
}
