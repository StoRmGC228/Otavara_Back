namespace API.Controllers;

using System.Security.Claims;
using Application.Interfaces;
using AutoMapper;
using Domain.DtoEntities;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
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
        var mappedEvents = _mapper.Map<List<EventWithIdDto>>(events);
        return Ok(mappedEvents);
    }

    //[Authorize]
    [HttpGet("paginated")]
    public async Task<IActionResult> GetPaginated(int pageSize, int pageNumber)
    {
        var paginatedEvents = await _eventService.GetPaginateAsync(pageSize, pageNumber);

        var mappedEvents = _mapper.Map<List<EventWithIdDto>>(paginatedEvents.PaginatedEntities);
        var result = new PaginatedEventsDto
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
        var result = _mapper.Map<EventWithIdDto>(eventEntity);
        if (eventEntity == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEvent([FromBody] EventWithoutIdDto newEvent)
    {
        var createdEvent = _mapper.Map<Event>(newEvent);
        var result = await _eventService.AddAsync(createdEvent);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] EventWithIdDto updatedEvent)
    {
        var existingEvent = await _eventService.GetByIdAsync(id);
        if (existingEvent == null)
        {
            return NotFound();
        }

        var updatedEntity = _mapper.Map<Event>(updatedEvent);
        await _eventService.UpdateAsync(updatedEntity, id);
        return Ok();
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

    [HttpPost("{userId}/{id}/participants")]
    public async Task<IActionResult> AddParticipant(Guid userId, Guid id)
    {
        await _eventService.AddParticipantAsync(id, userId);
        return Ok();
    }

    [Authorize]
    [HttpDelete("{id}/participants")]
    public async Task<IActionResult> UnsubscribeFromEvent(Guid id)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        await _eventService.RemoveParticipantAsync(id, userId);
        return NoContent();
    }

    [HttpDelete("{userId}/{eventId}/admin/participants")]
    public async Task<IActionResult> RemovePartisipantAsAdmin(Guid userId, Guid eventId)
    {
        await _eventService.RemoveParticipantAsync(eventId, userId);
        return NoContent();
    }


    [HttpGet("date")]
    public async Task<IActionResult> GetEventsByDate([FromQuery] DateTime date)
    {
        var events = await _eventService.GetEventsByDateAsync(date);
        return Ok(events);
    }

    [HttpGet("name/date")]
    public async Task<IActionResult> GetEventsByNameAndDate([FromQuery] SearchEventDto searchedEvent)
    {
        searchedEvent.StartDate = searchedEvent.StartDate.Value.Date;
        if (searchedEvent.EndDate != null)
        {
            var date = searchedEvent.EndDate.Value.Date;
            searchedEvent.EndDate = date.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);
        }
        var response = _mapper.Map<List<EventWithIdDto>>(await _eventService.GetEventsByNameAndDateRangeAsync(searchedEvent.Name, searchedEvent.StartDate,
            searchedEvent.EndDate));
        return Ok(response);
    }

}