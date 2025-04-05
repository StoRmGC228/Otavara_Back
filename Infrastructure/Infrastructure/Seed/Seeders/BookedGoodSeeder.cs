using Domain.Entities;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seed.Seeders
{
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

            var bookedGoods = new List<BookedGood>
            {
                new BookedGood
                {
                    UserId = users[0].Id,
                    GoodId = goods[0].Id,
                    BookingExpirationDate = DateTime.UtcNow.AddDays(3)
                },
                new BookedGood
                {
                    UserId = users[1].Id,
                    GoodId = goods[2].Id,
                    BookingExpirationDate = DateTime.UtcNow.AddDays(5)
                },
                new BookedGood
                {
                    UserId = users[2].Id,
                    GoodId = goods[4].Id,
                    BookingExpirationDate = DateTime.UtcNow.AddDays(7)
                },
                new BookedGood
                {
                    UserId = users[3].Id,
                    GoodId = goods[1].Id,
                    BookingExpirationDate = DateTime.UtcNow.AddDays(2)
                },
                new BookedGood
                {
                    UserId = users[4].Id,
                    GoodId = goods[3].Id,
                    BookingExpirationDate = DateTime.UtcNow.AddDays(6)
                },
                new BookedGood
                {
                    UserId = users[5].Id,
                    GoodId = goods[5].Id,
                    BookingExpirationDate = DateTime.UtcNow.AddDays(8)
                },
                new BookedGood
                {
                    UserId = users[6].Id,
                    GoodId = goods[6].Id,
                    BookingExpirationDate = DateTime.UtcNow.AddDays(4)
                },
                new BookedGood
                {
                    UserId = users[7].Id,
                    GoodId = goods[7].Id,
                    BookingExpirationDate = DateTime.UtcNow.AddDays(10)
                },
                new BookedGood
                {
                    UserId = users[8].Id,
                    GoodId = goods[8].Id,
                    BookingExpirationDate = DateTime.UtcNow.AddDays(1)
                },
                new BookedGood
                {
                    UserId = users[9].Id,
                    GoodId = goods[9].Id,
                    BookingExpirationDate = DateTime.UtcNow.AddDays(9)
                }
            };

            await dbContext.BookedGoods.AddRangeAsync(bookedGoods);
            await dbContext.SaveChangesAsync();
        }
    }
}
