namespace Application.Services;

using AutoMapper;
using Domain.DtoEntities;
using Domain.Entities;
using Interfaces;

public class AuthService : IAuthService
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public AuthService(IUserRepository userRepository, IJwtProvider jwtProvider,IMapper mapper)
    {
        _jwtProvider = jwtProvider;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<bool> VerifyDataCheckStringAsync(string telegramHash,string botTokenHash)
    {
        return telegramHash == botTokenHash;
    }

 

    public async Task<string> LoginUserAsync(TelegramUserDto loginUser)
    {
        var data = new SortedDictionary<string, string>()
        {
            { "auth_date", loginUser.Auth_date.ToString() },
            { "first_name", loginUser.First_name },
            { "id", loginUser.Id.ToString() },
            { "last_name", loginUser.Last_name },
            { "photo_url", loginUser.Photo_url },
            { "username", loginUser.Username }

        };
        string hashMaterial = string.Join("\n", data
            .Where(kv => kv.Value != null) 
            .OrderBy(kv => kv.Key)
            .Select(kv => $"{kv.Key}={kv.Value}"));


        var botTokenHash = Hashing.HashDataCheckString(hashMaterial);
        var result = await VerifyDataCheckStringAsync(loginUser.Hash,botTokenHash);
        if (result == false)
        {
            throw new Exception("something went wrong with authorization");
        }

        var user = new User()
        {
            First_name = loginUser.First_name,
            Last_name = loginUser.Last_name,
            TelegramId = loginUser.Id,
            Photo_url = loginUser.Photo_url,
            Username = loginUser.Username

        };
        var isExisting = await _userRepository.IsUserExisting(user);

        if (isExisting==false)
        {
            await _userRepository.AddUserAsync(user);
        }

        var token = await _jwtProvider.GenerateTokenAsync(user);
        return token;
    }
}