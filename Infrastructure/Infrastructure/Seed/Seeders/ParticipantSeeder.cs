namespace Infrastructure.Seed.Seeders;

using Application.Interfaces;
using Configurations;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class ParticipantSeeder : IDataSeeder
{
    private readonly OtavaraDbContext _dbContext;

    public ParticipantSeeder(OtavaraDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public int Priority => 3;

    public async Task<bool> HasDataAsync()
    {
        return await _dbContext.Participants.AnyAsync();
    }

    public async Task SeedAsync()
    {
        var users = await _dbContext.Users.ToListAsync();
        var events = await _dbContext.Events.ToListAsync();
        var participants = new List<Participant>();

        foreach (var evt in events)
        {
            var participantsCount = RandomDataGenerator.GetRandomInt(1, 5);
            var shuffledUsers = users.OrderBy(x => Guid.NewGuid()).Take(participantsCount);

            foreach (var user in shuffledUsers)
                participants.Add(new Participant
                {
                    UserId = user.Id,
                    EventId = evt.Id
                });
        }

        await _dbContext.Participants.AddRangeAsync(participants);
        await _dbContext.SaveChangesAsync();
    }
}