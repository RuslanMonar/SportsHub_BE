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
        public async Task<ActionResult<AuthDto>> Login(LoginDto loginDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthDto
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(a => a.ErrorMessage))
                });
            }

            var authResponse = await _authService.LoginAsync(loginDto.Email, loginDto.Password);
            if (authResponse.Success)
            {
                return Ok(new AuthDto
                {

                    Token = authResponse.Token
                });
            }
            else
            {
                return BadRequest(new AuthDto
                {
                    Errors = authResponse.Errors
                });
            }
        }


        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<AuthDto>> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthDto
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(a => a.ErrorMessage))
                });
            }

            var authResponse = await _authService.RegisterAsync(registerDto.Email, registerDto.Password,
                                        registerDto.FirstName, registerDto.LastName);

            if (authResponse.Success)
            {
                return Ok(new AuthDto
                {
                    Token = authResponse.Token
                });
            }
            else
            {
                return BadRequest(new
                {
                    Errors = authResponse.Errors
                });
            }
        }


        [HttpPost]
        [Route("changePassword")]
        public async Task<ActionResult<AuthDto>> ChangePassword(ChangePasswordDto changePasswordDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthDto
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(a => a.ErrorMessage))
                });
            }

            var authResponse = await _authService.ChangePasswordAsync(changePasswordDto.Email, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);
            if (authResponse.Success)
            {
                return Ok(new ChangePasswordResultDTO
                {
                    Status = authResponse.Success
                });
            }
            else
            {
                return BadRequest(new ChangePasswordResultDTO
                {
                    Errors = authResponse.Errors
                });
            }
        }
    }
}