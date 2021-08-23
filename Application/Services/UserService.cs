using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Application.Services
{
    public class UserService: IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _config;

        public UserService(UserManager<AppUser> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        public async Task<IResult> UpdateUserAsync(string id, string firstName, string lastName, string email, string imageUrl)
        {
            var user = await _userManager.FindByIdAsync(id);

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
    }
}