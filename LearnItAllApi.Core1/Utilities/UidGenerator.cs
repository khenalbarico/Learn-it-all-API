namespace LearnItAllApi.Core1.Utilities;

public static class UidGenerator
{
    public static string GenerateUid(this string prefix)
    {
        var random = new Random();
        int num    = random.Next(10000, 100000);

        return $"{prefix}-{num}";
    }
}
