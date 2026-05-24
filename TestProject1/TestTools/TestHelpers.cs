namespace TestProject1.TestTools;

public static class TestHelpers
{
    public static Stream GetFileStream(string fullPath)
    {
        if (!File.Exists(fullPath))
            throw new FileNotFoundException($"Test file not found: {fullPath}");
        return File.OpenRead(fullPath);
    }

    public static IEnumerable<string> GetFilePaths(string folderPath)
    {
        var baseDir    = AppContext.BaseDirectory;
        var fullFolder = Path.Combine(baseDir, folderPath);

        if (!Directory.Exists(fullFolder))
            throw new DirectoryNotFoundException($"Test folder not found: {fullFolder}");

        return Directory.EnumerateFiles(fullFolder, "*", SearchOption.AllDirectories);
    }

    public static string GetFolderPath(string relativePath)
    {
        var baseDir = AppContext.BaseDirectory;

        return Path.Combine(baseDir, "TestFiles", relativePath);
    }

    public static string GenerateUid(this string prefix)
    {
        var random = new Random();
        int num    = random.Next(10000, 100000);

        return $"{prefix}-{num}";
    }
}
