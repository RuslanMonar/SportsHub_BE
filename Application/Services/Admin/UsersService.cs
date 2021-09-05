using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Admin.Users;
using Application.Services.Admin.Interfaces;
using Domain;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Application.Services.Admin
{
    public class UsersService : IUsersService
    {
        public UserManager<AppUser> _userManager;
        private readonly IUserAccessorService _userAccessorService;

        public UsersService(UserManager<AppUser> userManager, IUserAccessorService userAccessorService)
        {
            _userManager = userManager;
            _userAccessorService = userAccessorService;
        }

        public async Task<SearchResult> SearchUserAsync(string name)
        {
            var users = new List<SearchUsersDto>();
            name = name.Trim(); //Видаляю пробіли спереду ззаді 

            if (!String.IsNullOrEmpty(name))
            {
                //Якшо name містить firstName lastName 
                if (name.Any(x => Char.IsWhiteSpace(x)))
                {
                    var names = Regex.Split(name, @"\s+"); //Видаляю всі пробіли між firstName і lastName
                    string firstName = names[0];
                    string lastName = names[1];
                    // Працює для "FirstName LastName" і для "LastName FirstName"
                    users = await _userManager.Users
                        .Where(user =>
                            (user.FirstName.Contains(firstName) && user.LastName.Contains(lastName)) ||
                            user.FirstName.Contains(lastName) && user.LastName.Contains(firstName))
                        .Select(user => new SearchUsersDto(ref user)).ToListAsync();
                }
                //Якшо name містить тільки firstName або lastName 
                else
                {
                    users = await _userManager.Users
                        .Where(user => user.FirstName.Contains(name) || user.LastName.Contains(name))
                        // Беру поля тіки ті які мені потрібно
                        .Select(user => new SearchUsersDto(ref user))
                        .ToListAsync();
                }
            }

            return new SearchResult
            {
                Success = true,
                Users = users,
            };
        }

        public async Task<SearchResult> SortUsersAsync(string type)
        {
            var users = new List<SearchUsersDto>();
            switch (type)
            {
                case "Active":
                    users = await _userManager.Users
                        .OrderByDescending(u => u.IsBlocked == false)
                        .Select(user => new SearchUsersDto(ref user)).ToListAsync();
                    break;
                case "Blocked":
                    users = await _userManager.Users
                        .OrderByDescending(u => u.IsBlocked == true)
                        .Select(user => new SearchUsersDto(ref user)).ToListAsync();
                    break;
                default:
                    users = await _userManager.Users
                        .Select(user => new SearchUsersDto(ref user)).ToListAsync();
                    break;
            }

            return new SearchResult
            {
                Success = true,
                Users = users,
            };
        }
        private async static Task<SearchUsersDto> GetUser(AppUser user, UserManager<AppUser> _userManager)
        {
            return new SearchUsersDto {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Image = user.Image,
                    IsBlocked = user.IsBlocked,
                    Role = await _userManager.GetRolesAsync(user) 
            };
        }
        public async Task<SearchResult> GetAllUsers()
        {
            var users = new List<SearchUsersDto>();
            var allUsersWithoutRoles = await  _userManager.Users.ToListAsync();
            foreach (var user in allUsersWithoutRoles)
            {              
                users.Add(await GetUser(user,_userManager));
            }

            return new SearchResult
            {
                Success = true,
                Users = users,
            };
        }


        public async Task<Result> ChangeStatus(string id)
        {
            string adminId = _userAccessorService.GetUserId();

            if (adminId == id)
            {
                return new Result
                {
                    Errors = new[] { "You cannot block yourself" },
                    Success = false
                };
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                if (user.IsBlocked)
                {
                    user.IsBlocked = false;
                }
                else
                {
                    user.IsBlocked = true;
                }

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return new Result
                    {
                        Success = true
                    };
                }
                else
                {
                    return new Result
                    {
                        Errors = new[] { "User was not blocked" },
                    };
                }
            }
            else
            {
                return new Result
                {
                    Errors = new[] { "There is no such a user" },
                };
            }
        }
    }
}