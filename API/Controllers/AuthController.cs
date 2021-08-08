using System;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(new UserDto
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(a => a.ErrorMessage))
                });
            }

            var authResponse = await _authService.LoginAsync(loginDto.Email, loginDto.Password);
            if (authResponse.Success)
            {
                return Ok(new UserDto
                {
                    Username = authResponse.Username,
                    Image = null,
                    Token = authResponse.Token
                });
            }
            else
            {
                return BadRequest(new UserDto
                {
                    Errors = authResponse.Errors
                });
            }
        }


        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new UserDto
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(a => a.ErrorMessage))
                });
            }

            var authResponse = await _authService.RegisterAsync(registerDto.Email, registerDto.Password, registerDto.Username);

            if (authResponse.Success)
            {
                return Ok(new UserDto
                {
                    Username = authResponse.Username,
                    Image = null,
                    Token = authResponse.Token
                });
            }
            else
            {
                return BadRequest(new UserDto
                {
                    Errors = authResponse.Errors
                });
            }
        }
    }
}