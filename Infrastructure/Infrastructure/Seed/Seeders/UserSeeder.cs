using Domain.Entities;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;

namespace Infrastructure.Seed.Seeders;

public class UserSeeder : IDataSeeder
{
    private readonly OtavaraDbContext _dbContext;

    public UserSeeder(OtavaraDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public int Priority => 1;

    public async Task<bool> HasDataAsync()
    {
        return await _dbContext.Users.AnyAsync();
    }

    public async Task SeedAsync()
    {
        var users = new List<User>();
        string[] roles = { "User", "User", "User", "User", "User", "User", "Moderator", "Admin" };

        for (int i = 0; i < 8; i++)
        {
            var firstName = RandomDataGenerator.GenerateFirstName();
            var lastName = RandomDataGenerator.GenerateLastName();

            users.Add(new User
            {
                Id = Guid.NewGuid(),
                TelegramId = (int)RandomDataGenerator.GenerateTelegramId(),
                FirstName = firstName,
                LastName = lastName,
                Username = RandomDataGenerator.GenerateUsername(firstName, lastName),
                PhotoUrl = RandomDataGenerator.GeneratePhotoUrl(),
                Role = roles[i]
            });
        }

        await _dbContext.Users.AddRangeAsync(users);
        await _dbContext.SaveChangesAsync();
    }
}