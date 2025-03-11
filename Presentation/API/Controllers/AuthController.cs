using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    using Application.Interfaces;
    using AutoMapper;
    using Domain.DtoEntities;
    using Domain.Entities;
    using Microsoft.AspNetCore.Authorization;

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public AuthController(IAuthService authService, IUserService userService, IMapper mapper)
        {
            _authService = authService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserDto newUser)
        {
            var isExist = await _userService.GetUserByLoginAsync(newUser.Login);
            if (isExist != null)
            {
                return BadRequest();
            }

            var user = _mapper.Map<User>(newUser);
            await _authService.RegisterUserAsync(user);
            return Ok(newUser);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDto loginUser)
        {
            var user = await _userService.GetUserByLoginAsync(loginUser.Login);
            if (user == null)
            {
                return NotFound();
            }

            var token = await _authService.LoginUserAsync(user, loginUser.HashPassword);
            HttpContext.Response.Cookies.Append("MySecretCookies", token);
            return Ok(new { token });
        }


    }
}
