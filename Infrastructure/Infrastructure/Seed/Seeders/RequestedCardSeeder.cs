using Domain.Entities;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seed.Seeders
{
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

            var cards = new List<RequestedCard>
            {
                new RequestedCard
                {
                    Id = Guid.Parse("d9e7c3b4-a5f6-4e2d-b1c8-a9d2e7f5c6b3"),
                    RequesterId = users[0].Id,
                    EventId = events[0].Id,
                    Link = "https://scryfall.com/card/dom/1/karn-scion-of-urza",
                    Code = "DOM",
                    Number = 1,
                    RequestedDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-5))
                },
                new RequestedCard
                {
                    Id = Guid.Parse("e8b7a6c5-d4f3-4e2d-b1c9-a8d7e6f5c4b3"),
                    RequesterId = users[1].Id,
                    EventId = events[0].Id,
                    Link = "https://scryfall.com/card/war/2/liliana-dreadhorde-general",
                    Code = "WAR",
                    Number = 2,
                    RequestedDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-3))
                },
                new RequestedCard
                {
                    Id = Guid.Parse("f7c6b5a4-e3d2-4c1b-b0a9-d8e7f6a5b4c3"),
                    RequesterId = users[2].Id,
                    EventId = events[0].Id,
                    Link = "https://scryfall.com/card/mh1/3/wrenn-and-six",
                    Code = "MH1",
                    Number = 3,
                    RequestedDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-1))
                },
                new RequestedCard
                {
                    Id = Guid.Parse("c2f87d36-34bc-45b1-b11c-3b9a0d31a00f"),
                    RequesterId = users[3].Id,
                    EventId = events[0].Id,
                    Link = "https://scryfall.com/card/thb/4/elspeth-heroes-downer",
                    Code = "THB",
                    Number = 4,
                    RequestedDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-10))
                },
                new RequestedCard
                {
                    Id = Guid.Parse("b3a7c9e5-5f7d-4b8f-b86f-c38502ac6cd2"),
                    RequesterId = users[4].Id,
                    EventId = events[0].Id,
                    Link = "https://scryfall.com/card/eld/5/teferi-time-raveler",
                    Code = "ELD",
                    Number = 5,
                    RequestedDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-7))
                },
                new RequestedCard
                {
                    Id = Guid.Parse("d4f8e4c5-38bf-4b8b-baa7-1a905bdb97ea"),
                    RequesterId = users[5].Id,
                    EventId = events[0].Id,
                    Link = "https://scryfall.com/card/mh2/6/yawgmoths-will",
                    Code = "MH2",
                    Number = 6,
                    RequestedDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-15))
                },
                new RequestedCard
                {
                    Id = Guid.Parse("e9d32f29-bf42-4c56-b38c-c93770e10f95"),
                    RequesterId = users[6].Id,
                    EventId = events[0].Id,
                    Link = "https://scryfall.com/card/war/7/nissa-who-shakes-the-world",
                    Code = "WAR",
                    Number = 7,
                    RequestedDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-3))
                },
                new RequestedCard
                {
                    Id = Guid.Parse("4b10ad60-e92f-44b4-b453-e9c56b078f9d"),
                    RequesterId = users[7].Id,
                    EventId = events[0].Id,
                    Link = "https://scryfall.com/card/thb/8/daxos-the-truthbearer",
                    Code = "THB",
                    Number = 8,
                    RequestedDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-8))
                },
                new RequestedCard
                {
                    Id = Guid.Parse("3b924de5-935f-4510-b83a-fd72b418adf1"),
                    RequesterId = users[8].Id,
                    EventId = events[0].Id,
                    Link = "https://scryfall.com/card/mh1/9/elf-bolt",
                    Code = "MH1",
                    Number = 9,
                    RequestedDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-12))
                },
                new RequestedCard
                {
                    Id = Guid.Parse("2c1db41a-37d2-4fd1-bd9b-9ab9ea50d8b6"),
                    RequesterId = users[9].Id,
                    EventId = events[0].Id,
                    Link = "https://scryfall.com/card/eld/10/arnor-the-witch",
                    Code = "ELD",
                    Number = 10,
                    RequestedDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-14))
                }
            };

            await dbContext.Cards.AddRangeAsync(cards);
            await dbContext.SaveChangesAsync();
        }
    }
}
