using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;

namespace Application.Services
{
    public class UserService: IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserAccessorService _userAccessorService;
        private readonly IConfiguration _config;

        public UserService(UserManager<AppUser> userManager, IConfiguration config, IUserAccessorService userAccessorService)
        {
            _userManager = userManager;
            _config = config;
            _userAccessorService = userAccessorService;
        }

        public async Task<Result> UpdateUserAsync(string firstName, string lastName, string email)
        {
            var user = await _userManager.FindByIdAsync(_userAccessorService.GetUserId());

            if (user == null)
            {
                return new Result {
                    Success = false,
                    Errors = new [] { "User does not exists." }
                };
            }

            user.FirstName = firstName;
            user.LastName = lastName;
            user.Email = email;

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                return new Result
                {
                    Success = false,
                    Errors = updateResult.Errors.Select(x => x.Description)
                };
            }

            return new Result
            {
                Success = true,
                Errors = null
            };

        }


        public async Task<AppUser> GetUserAsync()
        {

            var user = await _userManager.FindByIdAsync(_userAccessorService.GetUserId());

            if (user == null)
            {
                throw new Exception("User does not exists");
            }

            return new AppUser
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
              
            };
        }
        public async Task<Result> SwitchRolesAsync(string id)
        {
            string adminId = _userAccessorService.GetUserId();  

            if (adminId == id)
            {
                return new Result
                {
                    Errors = new[] { "You cannot downgrade yourself to user" },
                    Success = false
                };
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return new Result
                {
                    Errors = new[] { "There is no such a user" },
                    Success = false
                };
            }
            if (await _userManager.IsInRoleAsync(user, "Admin"))
            {
                await _userManager.RemoveFromRoleAsync(user, "Admin");
                await _userManager.AddToRoleAsync(user, "User");
                return new Result
                {
                    Success = true
                };
            }
            else
            {
                await _userManager.RemoveFromRoleAsync(user, "User");
                await _userManager.AddToRoleAsync(user, "Admin");
                return new Result
                {
                    Success = true
                };
            }
        }
    }
}