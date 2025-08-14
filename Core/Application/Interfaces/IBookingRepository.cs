namespace Application.Interfaces;

using Domain.DtoEntities;
using Domain.Entities;

public interface IBookingRepository
{
    Task ConfirmBookings(List<CloseBookingsDto> closeBookings);
    Task<BookedGood[]> GetAllBookingsAsync();
    Task<BookedGood> GetBookAsync(Guid userId, Guid goodId);
    Task<List<UserBookingsDto>> GetUserBookingsAsync(Guid userId);
    Task<List<BookedGood>> GetGoodBookingsAsync(Guid goodId);
    Task<bool> IsGoodAvailableAsync(Guid goodId);
    Task<bool> IsGoodBookedByUserAsync(Guid goodId, Guid userId);
    Task BookGoodsAsync(BookedGood[] bookedGood);
    Task CancelBookingAsync(Guid goodId, Guid userId);
    Task<int> GetAvailableQuantityAsync(Guid goodId);
    Task RemoveExpiredBookingsAsync(DateTime currentDate);
    Task UpdateBookAsync(BookedGood newGood);
}