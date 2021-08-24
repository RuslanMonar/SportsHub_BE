using System.Threading.Tasks;
using API.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;

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
                user.FirstName, user.LastName, user.Email, user.ImageUrl
                );

            if (!updateResult.Success)
            {
               
                return BadRequest(updateResult);
            }
            return Ok("Update was succeeded");
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var receivedUser = await _userService.GetUserAsync();
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