namespace Application.Services;

using AutoMapper;
using Domain.Entities;
using Interfaces;

public class UserService : BaseService<User>, IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper) : base(userRepository, mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<User> GetUserByTelegramUserNameAsync(string telegramUserName)
    {
        var user = await _userRepository.GetUserByTelegramUserNameAsync(telegramUserName);
        return user;
    }
}