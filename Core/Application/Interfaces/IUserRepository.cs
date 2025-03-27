namespace Application.Interfaces;

using Domain.Entities;

public interface IUserRepository : IBaseRepository<User>
{
    Task AddUserAsync(User entity);
    Task<User> GetUserByTelegramUserNameAsync(string telegramUserName);
    Task<bool> IsUserExisting(User user);
}