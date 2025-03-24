namespace API.Controllers;

using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEvents()
    {
        var events = await _eventService.GetAllAsync();
        return Ok(events);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEventById(Guid id)
    {
        var eventEntity = await _eventService.GetByIdAsync(id);
        if (eventEntity == null)
        {
            return NotFound();
        }
        return Ok(eventEntity);

    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateEvent([FromBody] Event newEvent)
    {
        var result = await _eventService.AddAsync(newEvent);
        return CreatedAtAction(nameof(GetEventById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] Event updatedEvent)
    {
        if (id != updatedEvent.Id)
        {
            return BadRequest();
        }

        var existingEvent = await _eventService.GetByIdAsync(id);
        if (existingEvent == null)
        {
            return NotFound();
        }

        var result = await _eventService.UpdateAsync(updatedEvent);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteEvent(Guid id)
    {
        var existingEvent = await _eventService.GetByIdAsync(id);
        if (existingEvent == null)
        {
            return NotFound();
        }

        await _eventService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("{id}/participants")]
    public async Task<IActionResult> GetEventParticipants(Guid id)
    {
        var participants = await _eventService.GetEventParticipantsAsync(id);
        return Ok(participants);
    }

    [HttpPost("{id}/participants")]
    [Authorize]
    public async Task<IActionResult> AddParticipant(Guid id)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        await _eventService.AddParticipantAsync(id, userId);
        return Ok();
    }

    [HttpDelete("{id}/participants")]
    [Authorize]
    public async Task<IActionResult> RemoveParticipant(Guid id)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        await _eventService.RemoveParticipantAsync(id, userId);
        return NoContent();
    }

    [HttpGet("game/{game}")]
    public async Task<IActionResult> GetEventsByGame(string game)
    {
        var events = await _eventService.GetEventsByGameAsync(game);
        return Ok(events);
    }

    [HttpGet("date")]
    public async Task<IActionResult> GetEventsByDate([FromQuery] DateTime date)
    {
        var events = await _eventService.GetEventsByDateAsync(date);
        return Ok(events);
    }
}