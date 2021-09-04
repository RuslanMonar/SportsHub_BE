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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    /*[Authorize(Roles = "Admin")]*/
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        [Route("SearchUser")]
        public async Task<ActionResult<SearchUserDto>> SearchUser(string name)
        {
            if (!String.IsNullOrEmpty(name))
            {
                try
                {
                    var users = await _usersService.SearchUserAsync(name);
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
        [Route("ChangeStatus")]
        public async Task<IActionResult> ChangeStatus(ChangeStatusDto data)
        {
            var status = await _usersService.ChangeStatus(data.Id);
            if (status.Success)
            {
                return Ok(new Result
                {
                    Success = status.Success
                });
            }
            else
            {
                return BadRequest(new Result
                {
                    Errors = status.Errors
                });
            }
        }

        [HttpPost]
        [Route("GetSortedUsers")]
        public async Task<ActionResult<SearchUserDto>> GetSortedUsers(SortTypesDto data)
        {
            if (Enum.IsDefined(typeof(SortTypesDto), data))
            {
                var type = ((SortTypesDto)data).ToString();
                try
                {
                    var sortResult = await _usersService.SortUsersAsync(type);
                    return Ok(sortResult);
                }
                catch (Exception e)
                {
                    return BadRequest(e);
                }
            }
            return BadRequest(new Result
            {
                Errors = new[]{"Can't sort users by this parameter"}
            });
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<ActionResult<SearchUserDto>> GetAllUsers()
        {
            try
            {
                var result = await _usersService.GetAllUsers();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}