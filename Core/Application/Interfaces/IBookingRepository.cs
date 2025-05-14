namespace Application.Interfaces;

using Domain.Entities;

public interface IBookingRepository
{
    Task<List<BookedGood>> GetUserBookingsAsync(Guid userId);
    Task<List<BookedGood>> GetGoodBookingsAsync(Guid goodId);
    Task<bool> IsGoodAvailableAsync(Guid goodId);
    Task<bool> IsGoodBookedByUserAsync(Guid goodId, Guid userId);
    Task BookGoodAsync(Guid goodId, Guid userId, DateTime expirationDate, int count);
    Task CancelBookingAsync(Guid goodId, Guid userId);
    Task<int> GetAvailableQuantityAsync(Guid goodId);
    Task RemoveExpiredBookingsAsync(DateTime currentDate);
}