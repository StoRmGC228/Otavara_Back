namespace Infrastructure.Seed.Seeders;

using Application.Interfaces;
using Configurations;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class EventSeeder : IDataSeeder
{
    private readonly OtavaraDbContext _dbContext;

    public EventSeeder(OtavaraDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public int Priority => 2;

    public async Task<bool> HasDataAsync()
    {
        return await _dbContext.Events.AnyAsync();
    }

    public async Task SeedAsync()
    {
        var events = new List<Event>();
        var startDate = DateTime.UtcNow;
        for (var i = 0; i < 8; i++)
            events.Add(new Event
            {
                Id = Guid.NewGuid(),
                Name = RandomDataGenerator.GenerateEventName(),
                Description = RandomDataGenerator.GenerateEventDescription(),
                Price = RandomDataGenerator.GetRandomInt(0, 2000),
                Format = RandomDataGenerator.GenerateEventFormat(),
                EventStartTime = RandomDataGenerator.GetRandomDate(startDate, startDate.AddDays(14)),
                Image = RandomDataGenerator.GenerateEventImage()
            });

        await _dbContext.Events.AddRangeAsync(events);
        await _dbContext.SaveChangesAsync();
    }
}