namespace Application.Interfaces;

using Domain.DtoEntities;

public interface IBookingService
{
    Task ConfirmBookings(List<CloseBookingsDto> closeBookings);
    Task<List<AdminBookingDto>> GetAllBookings();
    Task<List<UserBookingsDto>> GetUserBookingsAsync(Guid userId);
    Task BookGoodsAsync(BookedGoodDto[] bookedGood, Guid userId);
    Task CancelBookingAsync(Guid goodId, Guid userId);
    Task<bool> IsGoodAvailableAsync(Guid goodId);
    Task<int> GetAvailableQuantityAsync(Guid goodId);
    Task RemoveExpiredBookingsAsync();
}