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

namespace Application.Services.Admin
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<AppUser> _userManager;

        public UsersService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
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
                        .Select(user => new SearchUsersDto() // Беру поля тіки ті які мені потрібно
                        {
                            Id = user.Id,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Image = null
                        })
                        .ToListAsync();
                }
                //Якшо name містить тільки firstName або lastName 
                else
                {
                    users = await _userManager.Users
                        .Where(user => user.FirstName.Contains(name) || user.LastName.Contains(name))
                        // Беру поля тіки ті які мені потрібно
                        .Select(user =>
                            new SearchUsersDto()
                            {
                                Id = user.Id,
                                FirstName = user.FirstName,
                                LastName = user.LastName,
                                Image = null
                            })
                        .ToListAsync();
                }
            }

            return new SearchResult
            {
                Success = true,
                Users = users,
            };
        }
    }
}