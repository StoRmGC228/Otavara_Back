namespace API.Controllers;

using System.Security.Claims;
using Application.Interfaces;
using AutoMapper;
using Domain.DtoEntities;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly IEventService _eventService;
    private readonly IMapper _mapper;

    public EventController(IEventService eventService, IMapper mapper)
    {
        _mapper = mapper;
        _eventService = eventService;
    }


    [HttpGet]
    public async Task<IActionResult> GetAllEvents()
    {
        var events = await _eventService.GetAllAsync();
        var mappedEvents = _mapper.Map<List<EventCreationDto>>(events);
        return Ok(mappedEvents);
    }


    [HttpGet("paginated")]
    public async Task<IActionResult> GetPaginated(int pageSize, int pageNumber)
    {
        var paginatedEvents = await _eventService.GetPaginateAsync(pageSize, pageNumber);

        var mappedEvents = _mapper.Map<List<EventCreationDto>>(paginatedEvents.PaginatedEntities);
        var result = new PaginatedEventsDto()
        {
            TotalPages = paginatedEvents.TotalPages,
            PaginatedEntities = mappedEvents
        };

        return Ok(result);
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
    public async Task<IActionResult> CreateEvent([FromBody] EventCreationDto newEvent)
    {
        var createdEvent = _mapper.Map<Event>(newEvent);
        var result = await _eventService.AddAsync(createdEvent);
        return Ok(result);
    }

    [HttpPut("{id}")]
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
    public async Task<IActionResult> AddParticipant(Guid id, Guid userId)
    {
        await _eventService.AddParticipantAsync(id, userId);
        return Ok();
    }

    [HttpDelete("{id}/participants")]
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