namespace Application.Interfaces;

using Domain.DtoEntities;

public interface IBookingService
{
    Task<List<BookedGoodDto>> GetUserBookingsAsync(Guid userId);
    Task<bool> BookGoodAsync(Guid goodId, Guid userId, int count);
    Task CancelBookingAsync(Guid goodId, Guid userId);
    Task<bool> IsGoodAvailableAsync(Guid goodId);
    Task<int> GetAvailableQuantityAsync(Guid goodId);
    Task RemoveExpiredBookingsAsync();
}