namespace API.Controllers;

using System.Security.Claims;
using Application.Interfaces;
using AutoMapper;
using Cloudinary;
using Domain.DtoEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly CloudinaryUploader _cloudinaryUploader;
    private readonly IEventService _eventService;
    private readonly IMapper _mapper;

    public EventController(IEventService eventService, IMapper mapper, CloudinaryUploader cloudinaryUploader)
    {
        _mapper = mapper;
        _eventService = eventService;
        _cloudinaryUploader = cloudinaryUploader;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEvents()
    {
        var events = await _eventService.GetAllAsync();
        var mappedEvents = _mapper.Map<List<EventDto>>(events);
        return Ok(mappedEvents);
    }

    //[Authorize]
    [HttpGet("paginated")]
    public async Task<IActionResult> GetPaginated(int pageSize, int pageNumber)
    {
        var paginatedEvents = await _eventService.GetPaginateAsync(pageSize, pageNumber);

        var mappedEvents = _mapper.Map<List<EventDto>>(paginatedEvents.PaginatedEntities);
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
        var result = _mapper.Map<EventDto>(eventEntity);
        if (eventEntity == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEvent([FromBody] EventCreationDto newEvent)
    {
        var ev = await _eventService.AddAsync(newEvent);
        return Ok(ev);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] EventCreationDto updatedEvent)
    {
        var existingEvent = await _eventService.GetByIdAsync(id);
        if (existingEvent == null)
        {
            return NotFound();
        }

        var ev = await _eventService.UpdateAsync(updatedEvent, id);
        return Ok(ev);
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
        return Ok();
    }

    [HttpGet("{id}/participants")]
    public async Task<IActionResult> GetEventParticipants(Guid id)
    {
        var participants = await _eventService.GetEventParticipantsAsync(id);
        var mappedParticipants = _mapper.Map<List<ParticipantForEventDto>>(participants);
        return Ok(mappedParticipants);
    }

    [Authorize]
    [HttpPost("{id}/participants")]
    public async Task<IActionResult> AddParticipant(Guid id)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var result = await _eventService.AddParticipantAsync(id, userId);
        return Ok(_mapper.Map<EventDto>(result));
    }

    [Authorize]
    [HttpDelete("{id}/participants")]
    public async Task<IActionResult> UnsubscribeFromEvent(Guid id)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var result = await _eventService.RemoveParticipantAsync(id, userId);
        return Ok(_mapper.Map<EventDto>(result));
    }

    [HttpDelete("{userId}/{eventId}/admin/participants")]
    public async Task<IActionResult> RemoveParticipantAsAdmin(Guid userId, Guid eventId)
    {
        await _eventService.RemoveParticipantAsync(eventId, userId);
        return NoContent();
    }


    [HttpGet("dateRange")]
    public async Task<IActionResult> GetEventsByDateRange([FromQuery] SearchedEventsByDateOnlyDto searchedEvents)
    {
        if (searchedEvents.StartDate == null)
        {
            return BadRequest();
        }

        searchedEvents.StartDate = searchedEvents.StartDate.Value.Date;
        if (searchedEvents.EndDate != null)
        {
            var date = searchedEvents.EndDate.Value.Date;
            searchedEvents.EndDate = date.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);
        }

        var response =
            _mapper.Map<List<EventDto>>(
                await _eventService.GetEventsByDateRangeAsync(searchedEvents.StartDate, searchedEvents.EndDate));
        return Ok(response);
    }

    [HttpGet("name/date")]
    public async Task<IActionResult> GetEventsByNameAndDate([FromQuery] SearchEventByNameAndDateDto searchedEvent)
    {
        searchedEvent.StartDate = searchedEvent.StartDate.Value.Date;
        if (searchedEvent.EndDate != null)
        {
            var date = searchedEvent.EndDate.Value.Date;
            searchedEvent.EndDate = date.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);
        }

        var response = _mapper.Map<List<EventDto>>(await _eventService.GetEventsByNameAndDateRangeAsync(
            searchedEvent.Name, searchedEvent.StartDate,
            searchedEvent.EndDate));
        return Ok(response);
    }

    [HttpGet("user/events")]
    public async Task<IActionResult> GetUserEvents()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var result = _mapper.Map<List<EventDto>>(await _eventService.GetUserEventsAsync(userId));
        return Ok(result);
    }
}