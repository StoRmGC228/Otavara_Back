namespace API.Controllers;

using Application.Interfaces;
using AutoMapper;
using Domain.DtoEntities;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

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
        HttpContext.Response.Cookies.Append("MySecretCookies", token);
        return Ok(new { token });
    }
}