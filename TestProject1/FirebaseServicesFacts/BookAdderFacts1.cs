using LearnItAllApi.Infrastructure1.FirebaseServices.Authentication;
using LearnItAllApi.Infrastructure1.FirebaseServices.RealtimeDatabase;
using LearnItAllApi.Infrastructure1.FirebaseServices.Storage;
using TestProject1.TestTools;
using Xunit.Abstractions;

namespace TestProject1.FirebaseServicesFacts;

public class BookAdderFacts1 (ITestOutputHelper _ctx)
{
    [Fact] public async Task Post_Book()
    {
        var _storageSut = _ctx.Get<IFirebaseStorage>();
        var _rtDbSut    = _ctx.Get<IFirebaseRealtimeDb>();
        var _authSut    = _ctx.Get<IFirebaseAuth>();

        //Generate Uid for this book first EDIT LATER: Prefix
        var bookUid = TestHelpers.GenerateUid("IS");

        //Get IdToken from authentication
        var authPayload = new
        {
            Email    = "khenalbarico05@gmail.com",
            Password = "Antimage05-"
        };
        var idToken = await _authSut.SignInAsync(authPayload.Email, authPayload.Password);

        //Upload Book Display Image to Storage and get the download URL EDIT LATER: Path Segments
        var imageStream = TestHelpers.GetFileStream("CollegeCourses/InformationSystem/INFORMATION SYSTEM.png");
        var imageUrl    = await _storageSut.UploadAsync(
            fileStream: imageStream,
            contentType: "image/png",
            idToken: idToken,
            "DisplayImages", "CollegeCourses", $"{bookUid}", "INFORMATION SYSTEM.png"
        );
        _ctx.WriteLine($"Uploaded: INFORMATION SYSTEM.png → {imageUrl}");

        //Upload Book PDF/s to Storage EDIT LATER: Path Segments
        var pdfFolder = TestHelpers.GetFolderPath("CollegeCourses/InformationSystem/Data Warehousing");
        var pdfFiles  = Directory.GetFiles(pdfFolder, "*.pdf");
        foreach (var filePath in pdfFiles)
        {
                  var fileName = Path.GetFileName(filePath);
            using var pdf      = File.OpenRead(filePath);

            var pdfUrl = await _storageSut.UploadAsync(
                pdf,
                "application/pdf",
                idToken,
                "Books", "CollegeCourses", $"{bookUid}", fileName
            );
            _ctx.WriteLine($"Uploaded: {fileName} → {pdfUrl}");
        }

        //Upload Book Metadata to Realtime Database EDIT LATER: Path Segments
        var metadata = new
        {
            Uid         = bookUid,
            ImageUrl    = imageUrl,
            Title       = "Information System",
            Description = "A complete set of books to learn Information System. Take all ! Learn it all!",
            Price       = 49.0

        };

        await _rtDbSut.PutAsync(
            metadata,
            idToken,
            "Books", "CollegeCourses", bookUid
        );
    }
}
