namespace Application.Services;

using Domain.Entities;
using Interfaces;

public class BookingService : IBookingService
{
    private readonly int _bookingExpirationHours = 168;
    private readonly IBookingRepository _bookingRepository;

    public BookingService(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public async Task<List<BookedGood>> GetUserBookingsAsync(Guid userId)
    {
        return await _bookingRepository.GetUserBookingsAsync(userId);
    }

    public async Task<bool> BookGoodAsync(Guid goodId, Guid userId)
    {
        if (!await _bookingRepository.IsGoodAvailableAsync(goodId))
        {
            return false;
        }

        if (await _bookingRepository.IsGoodBookedByUserAsync(goodId, userId))
        {
            return true;
        }

        var expirationDate = DateTime.UtcNow.AddHours(_bookingExpirationHours);
        await _bookingRepository.BookGoodAsync(goodId, userId, expirationDate);

        return true;
    }

    public async Task CancelBookingAsync(Guid goodId, Guid userId)
    {
        await _bookingRepository.CancelBookingAsync(goodId, userId);
    }

    public async Task<bool> IsGoodAvailableAsync(Guid goodId)
    {
        return await _bookingRepository.IsGoodAvailableAsync(goodId);
    }

    public async Task<int> GetAvailableQuantityAsync(Guid goodId)
    {
        return await _bookingRepository.GetAvailableQuantityAsync(goodId);
    }

    public async Task RemoveExpiredBookingsAsync()
    {
        await _bookingRepository.RemoveExpiredBookingsAsync(DateTime.UtcNow);
    }
}