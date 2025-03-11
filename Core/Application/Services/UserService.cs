namespace Application.Services;

using Domain.Entities;
using Interfaces;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> GetUserByLoginAsync(string login)
    {
        var user = await _userRepository.GetUserByLoginAsync(login);
        return user;
    }
}