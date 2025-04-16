using Domain.Entities;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;

namespace Infrastructure.Seed.Seeders;

public class RequestedCardSeeder : IDataSeeder
{
    private readonly OtavaraDbContext _dbContext;

    public RequestedCardSeeder(OtavaraDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public int Priority => 3;

    public async Task<bool> HasDataAsync()
    {
        return await _dbContext.Cards.AnyAsync();
    }

    public async Task SeedAsync()
    {
        var users = await _dbContext.Users.ToListAsync();
        var events = await _dbContext.Events.Where(e => e.Game == "Magic: The Gathering").ToListAsync();
        if (!events.Any())
        {
            events = await _dbContext.Events.ToListAsync();
        }

        if (users.Count == 0 || events.Count == 0)
        {
            return;
        }

        var cards = new List<Card>();

        for (int i = 0; i < 8; i++)
        {
        }

        //await _dbContext.Cards.AddRangeAsync(cards);
        //await _dbContext.SaveChangesAsync();
    }
}
