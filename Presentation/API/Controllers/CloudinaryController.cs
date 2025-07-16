namespace API.Controllers;

using Application.Interfaces;
using Cloudinary;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class CloudinaryController : ControllerBase
{
    private readonly CloudinaryUploader _cloudinaryUploader;
    private readonly IEventService _eventService;

    public CloudinaryController(CloudinaryUploader cloudinaryUploader, IEventService eventService)
    {
        _cloudinaryUploader = cloudinaryUploader;
        _eventService = eventService;
    }

    [HttpGet("events/getImages")]
    public async Task<IActionResult> GetImagesOfEventsFromCloudinaryAsync()
    {
        var response = await _eventService.GetEventImagesFromCloudAsync();
        return Ok(response);
    }

    [HttpGet("events/signature")]
    public IActionResult GetEventSignature()
    {
        var result = _cloudinaryUploader.GenerateUploadEventSignature();
        return Ok(result);
    }

    [HttpGet("goods/signature")]
    public IActionResult GetGoodsSignature()
    {
        var result = _cloudinaryUploader.GenerateUploadGoodsSignature();
        return Ok(result);
    }
}