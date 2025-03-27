namespace Application.Interfaces;

using Domain.Entities;

public interface IUserService : IBaseService<User>
{
    Task<User> GetUserByTelegramUserNameAsync(string telegramUserName);
}