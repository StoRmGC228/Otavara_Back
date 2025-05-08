namespace Infrastructure.Seed.Seeders;

using Application.Interfaces;
using Configurations;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class BookedGoodSeeder : IDataSeeder
{
    private readonly OtavaraDbContext _dbContext;

    public BookedGoodSeeder(OtavaraDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public int Priority => 4;

    public async Task<bool> HasDataAsync()
    {
        return await _dbContext.BookedGoods.AnyAsync();
    }

    public async Task SeedAsync()
    {
        var users = await _dbContext.Users.ToListAsync();
        var goods = await _dbContext.Goods.ToListAsync();
        var bookedGoods = new List<BookedGood>();
        HashSet<(Guid UserId, Guid GoodId)> usedCombinations = new();
        var maxBookings = Math.Min(10, users.Count * goods.Count);
        var attempts = 0;
        var created = 0;

        while (created < maxBookings && attempts < 100)
        {
            attempts++;
            var userIndex = RandomDataGenerator.GetRandomInt(0, users.Count);
            var goodIndex = RandomDataGenerator.GetRandomInt(0, goods.Count);
            var userId = users[userIndex].Id;
            var goodId = goods[goodIndex].Id;

            // Check if this combination is already used
            if (!usedCombinations.Contains((userId, goodId)))
            {
                usedCombinations.Add((userId, goodId));
                bookedGoods.Add(new BookedGood
                {
                    UserId = userId,
                    GoodId = goodId,
                    BookingExpirationDate = RandomDataGenerator.GetRandomDate(
                        DateTime.UtcNow.AddDays(1),
                        DateTime.UtcNow.AddDays(10))
                });
                created++;
            }
        }

        foreach (var bookedGood in bookedGoods) _dbContext.BookedGoods.Add(bookedGood);

        await _dbContext.SaveChangesAsync();
    }
}