namespace Infrastructure.Seed.Seeders;

using Application.Interfaces;
using Configurations;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class GoodSeeder : IDataSeeder
{
    private readonly OtavaraDbContext _dbContext;

    public GoodSeeder(OtavaraDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public int Priority => 2;

    public async Task<bool> HasDataAsync()
    {
        return await _dbContext.Goods.AnyAsync();
    }

    public async Task SeedAsync()
    {
        var users = await _dbContext.Users.ToListAsync();

        if (users.Count == 0)
        {
            return;
        }

        var goods = new List<Good>();
        for (var i = 0; i < 8; i++)
            goods.Add(new Good
            {
                Id = Guid.NewGuid(),
                Name = RandomDataGenerator.GenerateGoodName(),
                Description = RandomDataGenerator.GenerateGoodDescription(),
                Price = Math.Round(RandomDataGenerator.GetRandomDouble(100, 2000), 2),
                QuantityInStock = RandomDataGenerator.GetRandomInt(1, 50),
                CreatedAt = RandomDataGenerator.GetRandomDate(DateTime.UtcNow.AddDays(-30), DateTime.UtcNow)
            });

        await _dbContext.Goods.AddRangeAsync(goods);
        await _dbContext.SaveChangesAsync();
    }
}