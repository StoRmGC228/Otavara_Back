using Domain.Entities;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seed.Seeders
{
    public class GoodSeeder : IDataSeeder
    {
        public int Priority => 2;

        public async Task<bool> HasDataAsync(OtavaraDbContext dbContext)
        {
            return await dbContext.Goods.AnyAsync();
        }

        public async Task SeedAsync(OtavaraDbContext dbContext)
        {
            var userId = await dbContext.Users.Select(u => u.Id).FirstOrDefaultAsync();

            if (userId == Guid.Empty)
            {
                userId = Guid.Parse("d3c8a794-55d7-4e40-a1c3-8f4b41f697f0");
            }

            var goods = new List<Good>
            {
                new Good
                {
                    Id = Guid.Parse("c2f3de9d-0e8a-4c4f-8e9c-a3f81eabc446"),
                    CustomerId = userId,
                    Name = "Бустер Magic: The Gathering",
                    Description = "Бустер останнього випуску MTG",
                    Price = 150.0,
                    QuantityInStock = 50,
                    CreatedAt = DateTime.UtcNow.AddDays(-10)
                },
                new Good
                {
                    Id = Guid.Parse("e5a12a4f-8b1d-4b1c-8a48-3c9b5c9d2a95"),
                    CustomerId = userId,
                    Name = "Колода Commander MTG",
                    Description = "Готова колода для формату Commander",
                    Price = 1200.0,
                    QuantityInStock = 5,
                    CreatedAt = DateTime.UtcNow.AddDays(-5)
                },
                new Good
                {
                    Id = Guid.Parse("f7c8b01e-d85a-4d87-b651-4c1a2de8eb7d"),
                    CustomerId = userId,
                    Name = "Набір кубиків для D&D",
                    Description = "Набір з 7 кубиків для настільних ігор",
                    Price = 300.0,
                    QuantityInStock = 20,
                    CreatedAt = DateTime.UtcNow.AddDays(-15)
                },
                new Good
                {
                    Id = Guid.Parse("a1c7e3b8-d5f2-4e9a-b0c4-d8e1f2a7c5b3"),
                    CustomerId = userId,
                    Name = "Книга правил D&D 5e",
                    Description = "Базова книга правил для D&D 5 редакції",
                    Price = 1000.0,
                    QuantityInStock = 8,
                    CreatedAt = DateTime.UtcNow.AddDays(-20)
                },
                new Good
                {
                    Id = Guid.Parse("b2d8f4e7-c6a5-4f3e-a9d2-c1b5a4e7d9c8"),
                    CustomerId = userId,
                    Name = "Фігурка Space Marine",
                    Description = "Колекційна фігурка для Warhammer 40K",
                    Price = 450.0,
                    QuantityInStock = 10,
                    CreatedAt = DateTime.UtcNow.AddDays(-8)
                },
                new Good
                {
                    Id = Guid.Parse("1c8dbad1-0407-4df0-9c6e-e1c8d028bc1a"),
                    CustomerId = userId,
                    Name = "Дошка для монополії",
                    Description = "Класична настільна гра Monopoly",
                    Price = 350.0,
                    QuantityInStock = 15,
                    CreatedAt = DateTime.UtcNow.AddDays(-4)
                },
                new Good
                {
                    Id = Guid.Parse("7f1bce2d-89b0-4c13-bc5a-385de4f073e7"),
                    CustomerId = userId,
                    Name = "Набір для карткової гри Pokémon",
                    Description = "Колекційний набір карт для гри Pokémon",
                    Price = 600.0,
                    QuantityInStock = 30,
                    CreatedAt = DateTime.UtcNow.AddDays(-3)
                },
                new Good
                {
                    Id = Guid.Parse("d34e47b0-4b97-4a12-a6d3-3f27bc3e11f0"),
                    CustomerId = userId,
                    Name = "Фігурка для Warhammer",
                    Description = "Колекційна фігурка для настільної гри Warhammer",
                    Price = 800.0,
                    QuantityInStock = 12,
                    CreatedAt = DateTime.UtcNow.AddDays(-2)
                },
                new Good
                {
                    Id = Guid.Parse("f9d42d7e-1f51-4097-b763-fc87a1d95cfa"),
                    CustomerId = userId,
                    Name = "Декорації для D&D",
                    Description = "Декорації для гри Dungeons & Dragons",
                    Price = 400.0,
                    QuantityInStock = 6,
                    CreatedAt = DateTime.UtcNow.AddDays(-6)
                },
                new Good
                {
                    Id = Guid.Parse("b8a79db3-538d-4f9b-a3c1-4f865312f3f2"),
                    CustomerId = userId,
                    Name = "Гральні кості для настільних ігор",
                    Description = "Набір кольорових гральних костей для настільних ігор",
                    Price = 150.0,
                    QuantityInStock = 25,
                    CreatedAt = DateTime.UtcNow.AddDays(-1)
                }
            };

            await dbContext.Goods.AddRangeAsync(goods);
            await dbContext.SaveChangesAsync();
        }
    }
}
