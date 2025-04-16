namespace API.Controllers;

using System.Security.Claims;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpGet("user")]
    public async Task<IActionResult> GetUserBookings()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var bookings = await _bookingService.GetUserBookingsAsync(userId);
        return Ok(bookings);
    }

    [HttpPost("{goodId}")]
    public async Task<IActionResult> BookGood(Guid goodId)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var result = await _bookingService.BookGoodAsync(goodId, userId);
        return Ok();
    }

    [HttpDelete("{goodId}")]
    public async Task<IActionResult> CancelBooking(Guid goodId)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        await _bookingService.CancelBookingAsync(goodId, userId);
        return NoContent();
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
}