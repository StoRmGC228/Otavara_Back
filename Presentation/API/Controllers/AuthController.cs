namespace API.Controllers;

using Application.Interfaces;
using AutoMapper;
using Domain.DtoEntities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public AuthController(IAuthService authService, IUserService userService, IMapper mapper)
    {
        _authService = authService;
        _userService = userService;
        _mapper = mapper;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] TelegramUserDto loginUser)
    {
        var token = await _authService.LoginUserAsync(loginUser);

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true, // ВАЖЛИВО: Працює лише через HTTPS
            SameSite = SameSiteMode.None,
            Expires = DateTimeOffset.UtcNow.AddDays(7), // або MaxAge = TimeSpan.FromDays(7)
            Path = "/"
        };

        Response.Cookies.Append("MySecretCookies", token, cookieOptions);
        // опціонально: не повертай token у тілі відповіді
        return Ok(); // або Ok(new { success = true })
    }
    [Authorize]
    [HttpGet("get")]
    public async Task<IActionResult> GetUserById()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var user = await _userService.GetByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }
        var response = _mapper.Map<UserGetDto>(user);
        return Ok(response);

    }


}