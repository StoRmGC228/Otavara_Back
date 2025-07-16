namespace API.Controllers;

using System.Security.Claims;
using Application.Interfaces;
using AutoMapper;
using Domain.DtoEntities;
using Domain.Entities;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AnnouncementController : ControllerBase
{
    private readonly IAnnouncementService _announcementService;
    private readonly IMapper _mapper;
    private readonly IRequestedCardService _requestedCardService;

    public AnnouncementController(IAnnouncementService announcementService, IMapper mapper,
        IRequestedCardService requestedCardService)
    {
        _requestedCardService = requestedCardService;
        _announcementService = announcementService;
        _mapper = mapper;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateAnnouncement([FromBody] AnnouncementCreationDto announcementDto)
    {
        var announcement = _mapper.Map<Announcement>(announcementDto);
        announcement.Card = (await _requestedCardService.IsRequestedCardExistsAsync(announcement.Card.Id)
            ? await _requestedCardService.GetByIdAsync(announcement.Card.Id)
            : announcement.Card)!;
        announcement.RequesterId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _announcementService.AddAsync(announcement);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAnnouncements()
    {
        var announcements = await _announcementService.GetAllAsync();
        var result = _mapper.Map<List<AnnouncementDto>>(announcements);

        return Ok(result);
    }

    [HttpGet("paginated")]
    public async Task<IActionResult> GetPaginated(int pageSize, int pageNumber)
    {
        var paginatedAnnouncements = await _announcementService.GetPaginateAsync(pageSize, pageNumber);
        var mappedEvents = _mapper.Map<List<AnnouncementDto>>(paginatedAnnouncements.PaginatedEntities);

        return Ok(new
        {
            paginatedAnnouncements.TotalPages,
            PaginatedEntities = mappedEvents
        });
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAnnouncementById(Guid id)
    {
        var announcementEntity = await _announcementService.GetByIdAsync(id);
        if (announcementEntity == null)
        {
            return NotFound();
        }

        var mappedAnnouncement = _mapper.Map<AnnouncementDto>(announcementEntity);

        return Ok(mappedAnnouncement);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAnnouncement(Guid id)
    {
        var existingEvent = await _announcementService.GetByIdAsync(id);
        if (existingEvent == null)
        {
            return NotFound();
        }

        await _announcementService.DeleteAsync(id);
        return Ok();
    }

    [HttpDelete("deleteJob")]
    public async Task<IActionResult> DeleteOverdueAnnoucements()
    {
        RecurringJob.AddOrUpdate(() => _announcementService.DeleteOverdueAnnouncements(), Cron.Daily);

        return Ok("Recurring job started, mails will send in every minute");
    }
}