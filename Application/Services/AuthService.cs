using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        public AuthService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AuthResult> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new AuthResult
                {
                    Errors = new[] { "User does not exist" },
                    Success = false
                };
            }

            bool isPasswordValid = await _userManager.CheckPasswordAsync(user, password);

            if (!isPasswordValid)
            {
                return new AuthResult
                {
                    Errors = new[] { "Password is wrong" },
                    Success = false
                };
            }

            return new AuthResult
            {
                Success = true,
                Token = "this will be a token",
                Username = user.UserName

            };
        }

        public async Task<AuthResult> RegisterAsync(string email, string password, string username)
        {
            if (await _userManager.FindByEmailAsync(email) != null)
            {
                return new AuthResult
                {
                    Errors = new[] { "User with this email already exists" },
                    Success = false
                };
            }

            var newUser = new IdentityUser
            {
                Email = email,
                UserName = username
            };

            var createdUser = await _userManager.CreateAsync(newUser, password);
            if (!createdUser.Succeeded)
            {
                return new AuthResult
                {
                    Errors = createdUser.Errors.Select(x => x.Description),
                    Success = false
                };
            }

            return new AuthResult
            {
                Success = true,
                Token = "this will be a token",
                Username = username
            };

        }
    }
}