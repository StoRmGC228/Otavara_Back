using Domain.Entities;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seed.Seeders
{
    public class ParticipantSeeder : IDataSeeder
    {
        
        public int Priority => 3;

        public async Task<bool> HasDataAsync(OtavaraDbContext dbContext)
        {
            return await dbContext.Participants.AnyAsync();
        }

        public async Task SeedAsync(OtavaraDbContext dbContext)
        {

            var users = await dbContext.Users.ToListAsync();
            var events = await dbContext.Events.ToListAsync();

            var participants = new List<Participant>();

            foreach (var evt in events)
            {
                var randomUsers = users.OrderBy(x => Guid.NewGuid()).Take(new Random().Next(1, 4));

                foreach (var user in randomUsers)
                {
                    participants.Add(new Participant
                    {
                        UserId = user.Id,
                        EventId = evt.Id
                    });
                }
            }

            await dbContext.Participants.AddRangeAsync(participants);
            await dbContext.SaveChangesAsync();

        }
    }
}