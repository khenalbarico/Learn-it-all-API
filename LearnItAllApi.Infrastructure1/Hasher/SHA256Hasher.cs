using System.Security.Cryptography;
using System.Text;

namespace LearnItAllApi.Infrastructure1.Hasher;

public static class SHA256Hasher
{
    public static string Hash256(this Stream fileStream)
    {
        if (fileStream.CanSeek)
            fileStream.Position = 0;

        using var    sha256    = SHA256.Create();
              byte[] hashBytes = sha256.ComputeHash(fileStream);
              var    builder   = new StringBuilder();

        foreach (byte b in hashBytes)
        {
            builder.Append(b.ToString("x2"));
        }

        return builder.ToString()[..8];
    }
}