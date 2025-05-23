namespace Application.Services;

using AutoMapper;
using Domain.DtoEntities;
using Domain.Entities;
using Interfaces;

public class AuthService : IAuthService
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository, IJwtProvider jwtProvider, IMapper mapper)
    {
        _jwtProvider = jwtProvider;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<bool> VerifyDataCheckStringAsync(string telegramHash, string botTokenHash)
    {
        return telegramHash == botTokenHash;
    }


    public async Task<string> LoginUserAsync(TelegramUserDto loginUser)
    {
        var data = new SortedDictionary<string, string>
        {
            { "auth_date", loginUser.AuthDate.ToString() },
            { "first_name", loginUser.FirstName },
            { "id", loginUser.TelegramId.ToString() },
            { "last_name", loginUser.LastName },
            { "photo_url", loginUser.PhotoUrl },
            { "username", loginUser.Username }
        };
        var hashMaterial = string.Join("\n", data
            .Where(kv => kv.Value != null)
            .OrderBy(kv => kv.Key)
            .Select(kv => $"{kv.Key}={kv.Value}"));


        var botTokenHash = Hashing.HashDataCheckString(hashMaterial);
        var result = await VerifyDataCheckStringAsync(loginUser.Hash, botTokenHash);
        if (result == false)
        {
            throw new Exception("something went wrong with authorization");
        }

        var user = _mapper.Map<User>(loginUser);

        var isExisting = await _userRepository.IsUserExisting(user);
        if (isExisting == false)
        {
            await _userRepository.AddUserAsync(user);
        }
        else
        {
            var existingUser = await _userRepository.GetUserByTelegramUserNameAsync(user.Username);
            if (existingUser != null)
            {
                user = existingUser;
            }
        }

        var token = await _jwtProvider.GenerateTokenAsync(user);
        return token;
    }
}