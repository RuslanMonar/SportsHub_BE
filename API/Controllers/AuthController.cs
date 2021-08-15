using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using Application.Services;
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

        [HttpPost]
        [Route("GoogleLogin")]
        public async Task<ActionResult<AuthDto>> GoogleLogin(UserGoogleDto user)
        {
            var authResponse = await _authService.AuthWithGoogleAsync(user.Email, user.Id, user.FirstName, user.LastName, user.ImageUrl);
            if (authResponse.Success)
            {
                return Ok(new AuthDto
                {

                    Token = authResponse.Token
                });
            }
            return BadRequest(new AuthDto
            {
                Errors = authResponse.Errors
            });
        }
    }


}