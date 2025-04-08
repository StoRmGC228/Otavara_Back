using Application.Interfaces;
using AutoMapper;
using Domain.DtoEntities;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {
        private readonly IAnnouncementService announcementService;
        private readonly IMapper _mapper;

        public AnnouncementController(IAnnouncementService announcementService, IMapper mapper)
        {
            this.announcementService = announcementService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnnouncement([FromBody] AnnouncementDto announcementDto)
        {
            var announcement = _mapper.Map<Announcement>(announcementDto);
            var result = await announcementService.AddAsync(announcement);
            return Ok(result);
        }
    }
}
