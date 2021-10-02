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

        [HttpPost]
        [Route("contactUs")]
        public async Task<IActionResult> ContactUs(ContactUsDto contactUsDto)
        {
            var result = await _userService.ContactUsAsync(contactUsDto.FirstName, contactUsDto.Email, contactUsDto.Phone, contactUsDto.Message);
           
            if (!result.Success)
            {
                return BadRequest(new Result
                {
                    Errors = result.Errors
                });
            }

            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
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