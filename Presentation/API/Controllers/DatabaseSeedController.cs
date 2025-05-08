namespace API.Controllers;

using Application.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class DatabaseSeedController : ControllerBase
{
    private readonly IHostEnvironment _environment;
    private readonly DatabaseSeedService _seedService;

    public DatabaseSeedController(
        DatabaseSeedService seedService,
        IHostEnvironment environment)
    {
        _seedService = seedService;
        _environment = environment;
    }

    [HttpPost("seed")]
    public async Task<IActionResult> SeedDatabase()
    {
        if (!_environment.IsDevelopment())
        {
            return StatusCode(403);
        }

        await _seedService.SeedDatabaseAsync();
        return Ok();
    }
}