using Domain.Entities;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seed.Seeders;

public class EventSeeder : IDataSeeder
{
    public int Priority => 2;

    public async Task<bool> HasDataAsync(OtavaraDbContext dbContext)
    {
        return await dbContext.Events.AnyAsync();
    }

    public async Task SeedAsync(OtavaraDbContext dbContext)
    {
        var events = new List<Event>();
        var startDate = DateTime.UtcNow;

        for (int i = 0; i < 8; i++)
        {
            events.Add(new Event
            {
                Id = Guid.NewGuid(),
                Name = RandomDataGenerator.GenerateEventName(),
                Description = RandomDataGenerator.GenerateEventDescription(),
                Price = RandomDataGenerator.GetRandomInt(0, 2000),
                Format = RandomDataGenerator.GenerateEventFormat(),
                Game = RandomDataGenerator.GenerateGameName(),
                EventStartTime = RandomDataGenerator.GetRandomDate(startDate, startDate.AddDays(14))
            });
        }

        await dbContext.Events.AddRangeAsync(events);
        await dbContext.SaveChangesAsync();
    }
}