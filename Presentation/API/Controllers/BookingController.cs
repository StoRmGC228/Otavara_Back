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

    [HttpGet("user/{id}")]
    public async Task<IActionResult> GetUserBookings(Guid id)
    {
        var isAdmin = User.IsInRole("Admin");
        var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        if (currentUserId == id || isAdmin)
        {
            var bookings = await _bookingService.GetUserBookingsAsync(id);
            return Ok(bookings);
        }

        return Forbid();
    }

    [HttpPost("{goodId}")]
    public async Task<IActionResult> BookGood(Guid goodId, int count)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _bookingService.BookGoodAsync(goodId, userId, count);
        return Ok();
    }

    [HttpDelete("{goodId}/{userId}")]
    public async Task<IActionResult> CancelBooking(Guid goodId, Guid userId)
    {
        var isAdmin = User.IsInRole("Admin");
        var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        if (currentUserId == userId || isAdmin)
        {
            await _bookingService.CancelBookingAsync(goodId, userId);
        }

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
}