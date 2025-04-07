using Domain.Entities;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seed.Seeders;
public class RequestedCardSeeder : IDataSeeder
{
    public int Priority => 3;
    public async Task<bool> HasDataAsync(OtavaraDbContext dbContext)
    {
        return await dbContext.Cards.AnyAsync();
    }
    public async Task SeedAsync(OtavaraDbContext dbContext)
    {
        var users = await dbContext.Users.ToListAsync();
        var events = await dbContext.Events.Where(e => e.Game == "Magic: The Gathering").ToListAsync();
        if (!events.Any())
        {
            events = await dbContext.Events.ToListAsync();
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

        await dbContext.Cards.AddRangeAsync(cards);
        await dbContext.SaveChangesAsync();
    }
}