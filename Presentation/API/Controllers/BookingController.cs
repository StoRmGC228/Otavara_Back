namespace API.Controllers;

using System.Security.Claims;
using Application.Interfaces;
using AutoMapper;
using Domain.DtoEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;
    private readonly IMapper _mapper;

    public BookingController(IBookingService bookingService, IMapper mapper)
    {
        _bookingService = bookingService;
        _mapper = mapper;
    }

    [Authorize]
    [HttpGet("user")]
    public async Task<IActionResult> GetUserBookings()
    {
        var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var bookings = await _bookingService.GetUserBookingsAsync(currentUserId);
        return Ok(bookings);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("admin")]
    public async Task<IActionResult> GetAllBookings()
    {
        return Ok(await _bookingService.GetAllBookings());
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> BookGoods([FromBody] BookedGoodDto[] bookings)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _bookingService.BookGoodsAsync(bookings, userId);
        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{goodId}/{userId}")]
    public async Task<IActionResult> CancelBooking(Guid goodId, Guid userId)
    {
        await _bookingService.CancelBookingAsync(goodId, userId);

        return Ok();
    }

    [HttpGet("{goodId}/available")]
    public async Task<IActionResult> IsGoodAvailable(Guid goodId)
    {
        var isAvailable = await _bookingService.IsGoodAvailableAsync(goodId);
        return Ok(new { isAvailable });
    }

    [HttpGet("{goodId}/quantity")]
    public async Task<IActionResult> GetAvailableQuantity(Guid goodId)
    {
        var availableQuantity = await _bookingService.GetAvailableQuantityAsync(goodId);
        return Ok(new { availableQuantity });
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("confirm")]
    public async Task<IActionResult> ConfirmBookings([FromBody] List<CloseBookingsDto> closeBookings)
    {
        await _bookingService.ConfirmBookings(closeBookings);
        return NoContent();
    }
}