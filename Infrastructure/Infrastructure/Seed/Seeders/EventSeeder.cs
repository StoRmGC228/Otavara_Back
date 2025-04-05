using Domain.Entities;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seed.Seeders
{
    public class EventSeeder : IDataSeeder
    {
        public int Priority => 2;

        public async Task<bool> HasDataAsync(OtavaraDbContext dbContext)
        {
            return await dbContext.Events.AnyAsync();
        }

        public async Task SeedAsync(OtavaraDbContext dbContext)
        {
            var startDate = DateTime.UtcNow;
            var events = new List<Event>
            {
                new Event
                {
                    Id = Guid.Parse("f0d8e683-a93b-43d1-a4c5-facc8d3441d3"),
                    Name = "Турнір з Magic: The Gathering",
                    Description = "Щомісячний турнір для новачків та просунутих гравців",
                    Price = 200,
                    Format = "Standard",
                    Game = "Magic: The Gathering",
                    EventStartTime = startDate.AddDays(7)
                },
                new Event
                {
                    Id = Guid.Parse("dbbe55d7-0c92-4586-a858-4204b6c7a7c4"),
                    Name = "Зустріч прихильників Dungeons & Dragons",
                    Description = "Щотижнева кампанія для досвідчених гравців",
                    Price = 100,
                    Format = "Campaign",
                    Game = "Dungeons & Dragons",
                    EventStartTime = startDate.AddDays(3)
                },
                new Event
                {
                    Id = Guid.Parse("e8799b2b-5d3a-4950-a528-2fa2d7ab0484"),
                    Name = "Турнір з Warhammer 40K",
                    Description = "Щомісячні змагання з Warhammer 40K",
                    Price = 300,
                    Format = "Tournament",
                    Game = "Warhammer 40K",
                    EventStartTime = startDate.AddDays(14)
                },
                new Event
                {
                    Id = Guid.Parse("c9e2a7cb-e55a-4cad-8d1a-3f89eaa23492"),
                    Name = "Розіграш карт Yu-Gi-Oh!",
                    Description = "Приходьте на розіграш рідкісних карт!",
                    Price = 1488,
                    Format = "Lottery",
                    Game = "Yu-Gi-Oh!",
                    EventStartTime = startDate.AddDays(2)
                },
                new Event
                {
                    Id = Guid.Parse("ce2e7d9e-7f97-43d8-a874-908c2fe764b3"),
                    Name = "Марафон з Hearthstone",
                    Description = "Марафон по грі Hearthstone для любителів карткових ігор",
                    Price = 150,
                    Format = "Marathon",
                    Game = "Hearthstone",
                    EventStartTime = startDate.AddDays(5)
                },
                new Event
                {
                    Id = Guid.Parse("7c654f98-d5f7-476f-9f9e-2681a2c5d7ff"),
                    Name = "Челендж з Runeterra",
                    Description = "Щомісячний челендж з Legends of Runeterra",
                    Price = 120,
                    Format = "Challenge",
                    Game = "Legends of Runeterra",
                    EventStartTime = startDate.AddDays(10)
                },
                new Event
                {
                    Id = Guid.Parse("4bb1cc72-9826-4d3c-8041-f09fd4ff59c8"),
                    Name = "Групова кампанія з Pathfinder",
                    Description = "Групова кампанія для новачків у світі Pathfinder",
                    Price = 666,
                    Format = "Campaign",
                    Game = "Pathfinder",
                    EventStartTime = startDate.AddDays(1)
                },
                new Event
                {
                    Id = Guid.Parse("a2a79a83-fd3c-4e60-b604-0a86fd1188f7"),
                    Name = "Зустріч любителів Pokemon TCG",
                    Description = "Зустріч для гравців та фанатів карткової гри Pokemon",
                    Price = 50,
                    Format = "Meetup",
                    Game = "Pokemon TCG",
                    EventStartTime = startDate.AddDays(4)
                },
                new Event
                {
                    Id = Guid.Parse("7bcb5de9-f80e-462b-89f7-d9b1b9b946c1"),
                    Name = "Картковий турнір з Gwent",
                    Description = "Турнір для всіх фанатів карткової гри Gwent",
                    Price = 250,
                    Format = "Tournament",
                    Game = "Gwent",
                    EventStartTime = startDate.AddDays(6)
                },
                new Event
                {
                    Id = Guid.Parse("be845263-2341-4e8f-b03f-b92f0da8d61f"),
                    Name = "Фестиваль настільних ігор",
                    Description = "Фестиваль з різноманітних настільних ігор для всієї родини",
                    Price = 0,
                    Format = "Festival",
                    Game = "Настільні ігри",
                    EventStartTime = startDate.AddDays(12)
                }
            };

            await dbContext.Events.AddRangeAsync(events);
            await dbContext.SaveChangesAsync();
        }
    }
}
