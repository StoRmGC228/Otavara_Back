using Domain.DtoEntities;

namespace Application.Services;

using AutoMapper;
using Domain.Entities;
using Interfaces;

public class BookingService : IBookingService
{
    private readonly int _bookingExpirationHours = 168;
    private readonly IMapper _mapper;
    private readonly IBookingRepository _bookingRepository;
    private readonly IGoodRepository _goodRepository;

    public BookingService(IBookingRepository bookingRepository, IMapper mapper, IGoodRepository goodRepository)
    {
        _bookingRepository = bookingRepository;
        _mapper = mapper;
        _goodRepository = goodRepository;
    }

    public async Task<List<BookedGoodDto>> GetUserBookingsAsync(Guid userId)
    {
        var userBooking = await _bookingRepository.GetUserBookingsAsync(userId);
        var bookedGoodDtos = _mapper.Map<List<BookedGoodDto>>(userBooking);
        return bookedGoodDtos;
    }

    public async Task<bool> BookGoodAsync(Guid goodId, Guid userId, int count)
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
        await _bookingRepository.BookGoodAsync(goodId, userId, expirationDate, count);
        await _goodRepository.DecreaseGoodQuantityAsync(goodId, count);

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