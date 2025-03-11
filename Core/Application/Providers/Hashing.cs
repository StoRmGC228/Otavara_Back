using System.Security.Cryptography;
using System.Text;

public static class Hashing
{
    public static string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(password);

            var hashBytes = sha256.ComputeHash(bytes);

            var builder = new StringBuilder();
            foreach (var b in hashBytes) builder.Append(b.ToString("x2"));

            return builder.ToString();
        }
    }
}