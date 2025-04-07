using Domain.Entities;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seed.Seeders;

public class BookedGoodSeeder : IDataSeeder
{
    public int Priority => 4;

    public async Task<bool> HasDataAsync(OtavaraDbContext dbContext)
    {
        return await dbContext.BookedGoods.AnyAsync();
    }

    public async Task SeedAsync(OtavaraDbContext dbContext)
    {
        var users = await dbContext.Users.ToListAsync();
        var goods = await dbContext.Goods.ToListAsync();
        var bookedGoods = new List<BookedGood>();
        HashSet<(Guid UserId, Guid GoodId)> usedCombinations = new HashSet<(Guid, Guid)>();

        int maxBookings = Math.Min(10, users.Count * goods.Count);
        int attempts = 0;
        int created = 0;

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

        foreach (var bookedGood in bookedGoods)
        {
            dbContext.BookedGoods.Add(bookedGood);
        }

        await dbContext.SaveChangesAsync();
    }
}