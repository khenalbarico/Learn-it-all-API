using System;

namespace TestProject1.TestTools;

internal static class TestHelpers
{
    public static Stream GetFileStream(string fileName)
    {
        string filePath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "TestFiles",
            fileName);

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException(
                $"Test file not found: {filePath}");
        }

        return File.OpenRead(filePath);
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
