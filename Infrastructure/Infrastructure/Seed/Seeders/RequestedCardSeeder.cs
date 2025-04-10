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

        var cards = new List<RequestedCard>();

        for (int i = 0; i < 8; i++)
        {
            cards.Add(new RequestedCard
            {
                Id = Guid.NewGuid(),
                RequesterId = users[RandomDataGenerator.GetRandomInt(0, users.Count - 1)].Id,
                EventId = events[RandomDataGenerator.GetRandomInt(0, events.Count - 1)].Id,
                Link = RandomDataGenerator.GenerateCardLink(),
                Code = RandomDataGenerator.GenerateCardCode(),
                Number = RandomDataGenerator.GetRandomInt(1, 100),
                RequestedDate = RandomDataGenerator.GetRandomDateOnly(
                    DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-30)),
                    DateOnly.FromDateTime(DateTime.UtcNow))
            });
        }

        await _dbContext.Cards.AddRangeAsync(cards);
        await _dbContext.SaveChangesAsync();
    }
}
