using System;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using Application;
using Application.FacebookResult;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<ActionResult<ChangePasswordResult>> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ChangePasswordResult
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(a => a.ErrorMessage))
                });
            }

            var authResponse = await _authService.ChangePasswordAsync(changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);
            if (authResponse.Status)
            {
                return Ok(new ChangePasswordResult
                {
                    Status = authResponse.Status
                });
            }
            else
            {
                return BadRequest(new ChangePasswordResult
                {
                    Errors = authResponse.Errors
                });
            }
        }

        [HttpPost]
        [Route("FBlogin")]
        public async Task<ActionResult<AuthDto>> FBLogin(UserFacebookDto request)
        {



            var authResponse = await _authService.LoginWithFacebookAsync(request.AccessToken);
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

        [HttpGet]
        [Route("user/get")]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var receivedUser = await _authService.GetUserAsync();
                return Ok(new
                { 
                FirstName = receivedUser.FirstName,
                LastName = receivedUser.LastName,
                ImageUrl = receivedUser.Image,
                Email = receivedUser.Email
                }
                );
            }
            catch (Exception exc)
            {
                return BadRequest(exc);
            }
        }
    }
}