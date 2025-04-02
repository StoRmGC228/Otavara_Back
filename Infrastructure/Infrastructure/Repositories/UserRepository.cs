namespace Infrastructure.Repositories;

using Application.Interfaces;
using Configurations;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly DbSet<User> _userDb;

    public UserRepository(OtavaraDbContext context) : base(context)
    {
        _userDb = context.Set<User>();
    }

    public async Task AddUserAsync(User entity)
    {
        await AddAsync(entity);
    }

    public async Task<User?> GetUserByTelegramUserNameAsync(string telegramUserName)
    {
        return await _userDb.FirstOrDefaultAsync(u => u.Username == telegramUserName);
    }

    public async Task<bool> IsUserExisting(User user)
    {
        return await _userDb.AnyAsync(u => u.TelegramId == user.TelegramId);
    }
}