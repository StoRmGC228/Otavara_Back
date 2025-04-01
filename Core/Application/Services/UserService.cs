namespace Application.Services;

using Domain.Entities;
using Interfaces;

public class UserService : BaseService<User>, IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository) : base(userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> GetUserByTelegramUserNameAsync(string telegramUserName)
    {
        var user = await _userRepository.GetUserByTelegramUserNameAsync(telegramUserName);
        return user;
    }
}