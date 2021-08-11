using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _config;
        private readonly IFacebookAuthService _facebookAuthService;
        public AuthService(UserManager<AppUser> userManager, IConfiguration config, IFacebookAuthService facebookAuthService)
        {
            _config = config;
            _userManager = userManager;
            _facebookAuthService = facebookAuthService;
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
                Token = CreateToken(user),
            };
        }

        public async Task<AuthResult> LoginWithFacebookAsync(string accessToken)
        {
            var validatedTokenResult = await _facebookAuthService.ValidateAccessTokenAsync(accessToken);
            if(!validatedTokenResult.Data.IsValid)
            {
                return new AuthResult
                {
                    Errors = new[] { "Invalid Facebook Token!" }
                };
            }
            var userInfo = await _facebookAuthService.GetUserInfoAsync(accessToken);
            var user = await _userManager.FindByEmailAsync(userInfo.Email);

            if(user == null)
            {
                var newUser = new AppUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = userInfo.Email,
                    FirstName = userInfo.FirstName,
                    LastName = userInfo.LastName,
                    UserName = userInfo.Email

                };
                var createdResult = await _userManager.CreateAsync(newUser);
                if(!createdResult.Succeeded)
                {
                    return new AuthResult
                    {
                        Errors = new[] { "Something went wrong..." }
                    };
                }
                else
                {
                    return new AuthResult
                    {
                        Success = true,
                        Token = CreateToken(newUser),
                    };
                }
            }
            else
            {
                return new AuthResult
                {
                    Success = true,
                    Token = CreateToken(user),
                };
            }
        }

        public async Task<AuthResult> RegisterAsync(string email, string password, string firstName, string LastName)
        {
            if (await _userManager.FindByEmailAsync(email) != null)
            {
                return new AuthResult
                {
                    Errors = new[] { "User with this email already exists" },
                    Success = false
                };
            }

            var newUser = new AppUser
            {
                Email = email,
                FirstName = firstName,
                LastName = LastName,
                UserName = email


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
                Token = CreateToken(newUser),

            };

        }

        private string CreateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.FirstName+" "+user.LastName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email)
            };

            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("say_hello_to_my_little_friend"));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TokenKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = credentials

            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }

        public async Task<UserResult> GetUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                throw new Exception("User does not exists");
            }

            return new UserResult
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Image = user.Image
            };
        }
    }
}