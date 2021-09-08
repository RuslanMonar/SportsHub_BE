using System.Threading.Tasks;
using API.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using Application;
using Microsoft.AspNetCore.Identity;
using Domain;
using Application.Services.User;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
                    Email = receivedUser.Email,
                    IsAdmin = receivedUser.IsAdmin
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