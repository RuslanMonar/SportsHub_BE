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

        public async Task<IResult> UpdateUserAsync(string firstName, string lastName, string email, string imageUrl)
        {
            var user = await _userManager.FindByIdAsync(_userAccessorService.GetUserId());

            if (user == null)
            {
                return new StandartResult {
                    Success = false,
                    Errors = new List<string> { "User does not exists." }
                };
            }

            user.FirstName = firstName;
            user.LastName = lastName;
            user.Email = email;
            user.Image = imageUrl;

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                return new StandartResult
                {
                    Success = false,
                    Errors = updateResult.Errors.Select(x => x.Description)
                };
            }

            return new StandartResult
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
                Image = user.Image
            };
        }
    }
}