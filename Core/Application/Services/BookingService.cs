namespace Application.Services;

using AutoMapper;
using Domain.DtoEntities;
using Domain.Entities;
using Interfaces;

public class BookingService : IBookingService
{
    private readonly int _bookingExpirationHours = 168;
    private readonly IBookingRepository _bookingRepository;
    private readonly IGoodRepository _goodRepository;
    private readonly IMapper _mapper;

    public BookingService(IBookingRepository bookingRepository, IMapper mapper, IGoodRepository goodRepository)
    {
        _bookingRepository = bookingRepository;
        _mapper = mapper;
        _goodRepository = goodRepository;
    }

    public async Task<List<AdminBookingDto>> GetAllBookings()
    {
        var allBookings = await _bookingRepository.GetAllBookingsAsync();
        List<AdminBookingDto> res = new();
        foreach (var bookGood in allBookings)
            res.Add(new AdminBookingDto
            {
                CountOfBooking = bookGood.Count,
                CreatedAt = bookGood.CreatedAt,
                GoodImage = bookGood.Good.Image,
                GoodName = bookGood.Good.Name,
                UserImage = bookGood.User.PhotoUrl,
                BookerUsername = bookGood.User.Username,
                BookerFirstName = bookGood.User.FirstName,
                GoodId = bookGood.GoodId,
                UserId = bookGood.UserId
            });

        return res;
    }

    public async Task<List<UserBookingsDto>> GetUserBookingsAsync(Guid userId)
    {
        return await _bookingRepository.GetUserBookingsAsync(userId);
    }

    public async Task BookGoodsAsync(BookedGoodDto[] bookedGoods, Guid userId)
    {
        var newBookings = new List<BookedGoodDto>();

        foreach (var item in bookedGoods)
        {
            item.UserId = userId;

            if (!await _bookingRepository.IsGoodAvailableAsync(item.GoodId))
            {
                throw new Exception($"Товару {item.Name} наразі немає в наявності, будь ласка, спробуйте пізніше.");
            }

            // списуємо кількість товару завжди
            await _goodRepository.DecreaseGoodQuantityAsync(item.GoodId, item.Count);

            if (await _bookingRepository.IsGoodBookedByUserAsync(item.GoodId, userId))
            {
                var existingBooking = await _bookingRepository.GetBookAsync(userId, item.GoodId);
                if (existingBooking != null)
                {
                    existingBooking.Count += item.Count;
                    existingBooking.BookingExpirationDate = DateTime.UtcNow.AddHours(_bookingExpirationHours);
                    existingBooking.CreatedAt = DateTime.UtcNow;
                    await _bookingRepository.UpdateBookAsync(existingBooking);
                }
            }
            else
            {
                item.BookingExpirationDate = DateTime.UtcNow.AddHours(_bookingExpirationHours);
                item.CreatedAt = DateTime.UtcNow;
                newBookings.Add(item);
            }
        }

        if (newBookings.Any())
        {
            var mapped = _mapper.Map<BookedGood[]>(newBookings);
            await _bookingRepository.BookGoodsAsync(mapped);
        }
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

    public async Task ConfirmBookings(List<CloseBookingsDto> closeBookings)
    {
        try
        {
            await _bookingRepository.ConfirmBookings(closeBookings);
        }
        catch (Exception err)
        {
            throw new Exception("Помилка при підтвердженні бронювання", err);
        }
    }
}