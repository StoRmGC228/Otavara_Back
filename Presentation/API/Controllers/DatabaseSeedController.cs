using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DatabaseSeedController : ControllerBase
    {
        private readonly DatabaseSeedService _seedService;
        private readonly IHostEnvironment _environment;

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
}