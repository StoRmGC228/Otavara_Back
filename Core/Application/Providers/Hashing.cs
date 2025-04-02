using System.Security.Cryptography;
using System.Text;

public static class Hashing
{
    public static string HashDataCheckString(string dataCheckString)
    {
        var botToken = Environment.GetEnvironmentVariable("BOT_TOKEN");

        if (string.IsNullOrEmpty(botToken))
        {
            throw new InvalidOperationException("BOT_TOKEN is not set in environment variables.");
        }

        using (var sha256 = SHA256.Create())
        {
            var secretKey = sha256.ComputeHash(Encoding.UTF8.GetBytes(botToken));

            using (var hmac = new HMACSHA256(secretKey))
            {
                var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(dataCheckString));

                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}