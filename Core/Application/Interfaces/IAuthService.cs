namespace Application.Interfaces;

using Domain.Entities;

public interface IAuthService
{
    Task RegisterUserAsync(User entity);
    Task<string> LoginUserAsync(User user, string password);
    Task<bool> VerifyPasswordAsync(User user, string password);
}