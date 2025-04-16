namespace Infrastructure.Repositories;

using Application.Interfaces;
using Configurations;
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

    public async Task<List<BookedGood>> GetUserBookingsAsync(Guid userId)
    {
        return await _bookedGoodDb
            .Include(bg => bg.Good)
            .Where(bg => bg.UserId == userId)
            .ToListAsync();
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
        var good = await _goodDb.FindAsync(goodId);
        if (good == null)
        {
            return false;
        }

        var bookedCount = await _bookedGoodDb.CountAsync(bg => bg.GoodId == goodId);
        return good.QuantityInStock > bookedCount;
    }

    public async Task<bool> IsGoodBookedByUserAsync(Guid goodId, Guid userId)
    {
        return await _bookedGoodDb.AnyAsync(bg => bg.GoodId == goodId && bg.UserId == userId);
    }

    public async Task BookGoodAsync(Guid goodId, Guid userId, DateTime expirationDate)
    {
        var booking = new BookedGood
        {
            GoodId = goodId,
            UserId = userId,
            BookingExpirationDate = expirationDate
        };

        await _bookedGoodDb.AddAsync(booking);
        await _context.SaveChangesAsync();
    }

    public async Task CancelBookingAsync(Guid goodId, Guid userId)
    {
        var booking = await _bookedGoodDb.FirstOrDefaultAsync(
            bg => bg.GoodId == goodId && bg.UserId == userId);

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

        var bookedCount = await _bookedGoodDb.CountAsync(bg => bg.GoodId == goodId);
        return good.QuantityInStock - bookedCount;
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
}