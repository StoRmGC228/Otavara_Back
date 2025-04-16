using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entities;
using Application.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Application.Providers;

public class JwtProvider : IJwtProvider
{

    public async Task<string> GenerateTokenAsync(User user)
    {
        var secret = Environment.GetEnvironmentVariable("JWT_SECRET");
        var issuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? "DefaultIssuer";
        var audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? "DefaultAudience";

        if (string.IsNullOrWhiteSpace(secret))
            throw new InvalidOperationException("JWT_SECRET is not set in environment variables.");

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Username),
            new(ClaimTypes.Role, user.Role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(3), // Можна винести в окрему змінну
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);

    }
}