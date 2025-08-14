namespace Infrastructure.Repositories;

using Application.Interfaces;
using Configurations;
using Domain.DtoEntities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class BookingRepository : IBookingRepository
{
    private readonly DbSet<BookedGood> _bookedGoodDb;
    private readonly OtavaraDbContext _context;
    private readonly DbSet<Good> _goodDb;

    public BookingRepository(OtavaraDbContext context)
    {
        _context = context;
        _bookedGoodDb = context.Set<BookedGood>();
        _goodDb = context.Set<Good>();
    }

    public async Task<BookedGood> GetBookAsync(Guid userId, Guid goodId)
    {
        var result = await _bookedGoodDb.FirstOrDefaultAsync(b => b.GoodId == goodId && b.UserId == userId);
        if (result == null)
        {
            throw new Exception("Щось пішло не так із вашим бронюванням, будь ласка зверніться до адміністрації");
        }

        return result;
    }

    public async Task<BookedGood[]> GetAllBookingsAsync()
    {
        return await _context.BookedGoods.OrderBy(g => g.CreatedAt).Include(g => g.User).Include(g => g.Good)
            .ToArrayAsync();
    }

    public async Task<List<UserBookingsDto>> GetUserBookingsAsync(Guid userId)
    {
        var req = await _bookedGoodDb
            .Include(bg => bg.Good)
            .Where(bg => bg.UserId == userId)
            .ToArrayAsync();
        List<UserBookingsDto> result = new();
        for (var i = 0; i < req.Length; i++)
            result.Add(new UserBookingsDto
            {
                Count = req[i].Count,
                Description = req[i].Good.Description,
                Id = req[i].GoodId,
                Image = req[i].Good.Image,
                Name = req[i].Good.Name
            });

        return result;
    }

    public async Task<List<BookedGood>> GetGoodBookingsAsync(Guid goodId)
    {
        return await _bookedGoodDb
            .Include(bg => bg.User)
            .Where(bg => bg.GoodId == goodId)
            .ToListAsync();
    }

    public async Task<bool> IsGoodAvailableAsync(Guid goodId)
    {
        return await GetAvailableQuantityAsync(goodId) > 0;
    }

    public async Task<bool> IsGoodBookedByUserAsync(Guid goodId, Guid userId)
    {
        return await _bookedGoodDb.AnyAsync(bg => bg.GoodId == goodId && bg.UserId == userId);
    }

    public async Task BookGoodsAsync(BookedGood[] bookedGoods)
    {
        await _bookedGoodDb.AddRangeAsync(bookedGoods);
        await _context.SaveChangesAsync();
    }

    public async Task CancelBookingAsync(Guid goodId, Guid userId)
    {
        var booking = await _bookedGoodDb.FirstOrDefaultAsync(
            bg => bg.GoodId == goodId && bg.UserId == userId);
        var newQuantityGood = await _goodDb.FirstOrDefaultAsync(g => g.Id == booking.GoodId);
        newQuantityGood.QuantityInStock += booking.Count;
        _goodDb.Update(newQuantityGood);
        if (booking != null)
        {
            _bookedGoodDb.Remove(booking);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<int> GetAvailableQuantityAsync(Guid goodId)
    {
        var good = await _goodDb.FindAsync(goodId);
        if (good == null)
        {
            return 0;
        }

        return good.QuantityInStock;
    }

    public async Task RemoveExpiredBookingsAsync(DateTime currentDate)
    {
        var expiredBookings = await _bookedGoodDb
            .Where(bg => bg.BookingExpirationDate < currentDate)
            .ToListAsync();

        if (expiredBookings.Any())
        {
            _bookedGoodDb.RemoveRange(expiredBookings);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateBookAsync(BookedGood newBooking)
    {
        _context.Attach(newBooking);
        _context.Entry(newBooking).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task ConfirmBookings(List<CloseBookingsDto> closeBookings)
    {
        // 1. Project the necessary properties into an anonymous type
        var userIds = closeBookings.Select(cb => cb.UserId).ToList();
        var goodIds = closeBookings.Select(cb => cb.GoodId).ToList();

        // 2. Query the database using a translatable 'Where' clause with 'Contains'
        var deletedGoods = await _bookedGoodDb
            .Where(b => userIds.Contains(b.UserId) && goodIds.Contains(b.GoodId))
            .ToListAsync();
        _bookedGoodDb.RemoveRange(deletedGoods);
        await _context.SaveChangesAsync();
    }
}