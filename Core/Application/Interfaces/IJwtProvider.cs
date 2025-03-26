namespace Application.Interfaces;

using Domain.Entities;

public interface IJwtProvider
{
    Task<string> GenerateTokenAsync(User user);
}