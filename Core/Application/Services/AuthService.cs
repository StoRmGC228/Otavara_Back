namespace Application.Services;

using Domain.Entities;
using Interfaces;

public class AuthService : IAuthService
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository, IJwtProvider jwtProvider)
    {
        _jwtProvider = jwtProvider;
        _userRepository = userRepository;
    }

    public async Task<bool> VerifyPasswordAsync(User user, string password)
    {
        return password == user.HashPassword;
    }

    public async Task RegisterUserAsync(User entity)
    {
        await _userRepository.AddUserAsync(entity);
    }

    public async Task<string> LoginUserAsync(User user, string password)
    {
        var userPassword = Hashing.HashPassword(password);
        var result = await VerifyPasswordAsync(user, userPassword);
        if (result == false)
        {
            throw new Exception("Wrong password");
        }

        var token = await _jwtProvider.GenerateTokenAsync(user);
        return token;
    }
}