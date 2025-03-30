namespace Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBookingService
{
    Task<List<BookedGood>> GetUserBookingsAsync(Guid userId);
    Task<bool> BookGoodAsync(Guid goodId, Guid userId);
    Task CancelBookingAsync(Guid goodId, Guid userId);
    Task<bool> IsGoodAvailableAsync(Guid goodId);
    Task<int> GetAvailableQuantityAsync(Guid goodId);
    Task RemoveExpiredBookingsAsync();
}