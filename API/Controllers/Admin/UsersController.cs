using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.Admin.Users;
using Application;
using Application.Services.Admin.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _userService;

        public UsersController(IUsersService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("SearchUser")]
        public async Task<ActionResult<SearchUserDto>> SearchUser(string name)
        {
            if (!String.IsNullOrEmpty(name))
            {
                try
                {
                    var users = await _userService.SearchUserAsync(name);
                    return Ok(users);
                }
                catch (Exception e)
                {
                    return BadRequest(e);
                }
            }
            return BadRequest(new Result
            {
                Errors = new[] {"Search name is empty"}
            });
        }

        [HttpPut]
        [Route("SwitchRoles")]
        public async Task<IActionResult> SwitchRoles(UpdateToAdminResult updateToAdmin)
        {
            try
            {
                var SwitchRoleUser = await _userService.SwitchRolesAsync(updateToAdmin.id);
                if (SwitchRoleUser.Success)
                {
                    return Ok(new Result
                    {
                        Success = SwitchRoleUser.Success
                    });
                }
                else
                {
                    return BadRequest(new Result
                    {
                        Errors = SwitchRoleUser.Errors
                    });
                }

            }
            catch (Exception exc)
            {
                return BadRequest(exc);
            }
        }
    }
}