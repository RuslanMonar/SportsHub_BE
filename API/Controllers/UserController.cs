using System.Threading.Tasks;
using API.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using Application;
using Microsoft.AspNetCore.Identity;
using Domain;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // postman tests [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        public UserController(IUserService userService, UserManager<AppUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateInfo(UserDto user)
        {
            var updateResult = await _userService.UpdateUserAsync(
                user.FirstName, user.LastName, user.Email
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
                return Ok(new UserDto
                {
                    FirstName = receivedUser.FirstName,
                    LastName = receivedUser.LastName,
                    Email = receivedUser.Email
                }
                );
            }
            catch (Exception exc)
            {
                return BadRequest(exc);
            }
        }

        
        [HttpPost]
        [Route("GiveAdminPermission")]
        public async Task<IActionResult> AddAdmin(UpdateToAdminResult updateToAdmin)
        {
            var user = await _userManager.FindByEmailAsync(updateToAdmin.Email);
            
            if (user == null) return NotFound();
            await _userManager.RemoveFromRoleAsync(user, "User");
            await _userManager.AddToRoleAsync(user, "Admin");

            return Ok(user);
        }


        [HttpPost]
        [Route("RemoveAdminPermission")]
        public async Task<IActionResult> RemoveAdmin(UpdateToAdminResult removeAdmin)
        {
            var user = await _userManager.FindByEmailAsync(removeAdmin.Email);

            if (user == null) return NotFound();
            await _userManager.RemoveFromRoleAsync(user, "Admin");
            await _userManager.AddToRoleAsync(user, "User");

            return Ok(user);
        }

    }
}