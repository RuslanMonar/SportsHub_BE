using System.Threading.Tasks;
using API.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateInfo(UserDto user)
        {
            var updateResult = await _userService.UpdateUserAsync(
                user.Id, user.FirstName, user.LastName, user.Email, user.ImageUrl
                );

            if (!updateResult.Success)
            {
               
                return BadRequest(updateResult);
            }
            return Ok("Update was succeeded");
        }

    }
}