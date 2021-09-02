using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Bogus;
using Bogus.DataSets;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Faker
{
    public class UsersFaker
    {
        public static async Task GenerateUsers(UserManager<AppUser> userManager)
        {
            if (userManager.Users.Count() < 50)
            {
                var userFaker = new Faker<AppUser>()
                    .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                    .RuleFor(o => o.LastName, f => f.Name.LastName())
                    .RuleFor(o => o.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
                    .RuleFor(o => o.UserName, (f, u) => f.Internet.Email(u.FirstName, u.LastName));
                    //.RuleFor(u => u.Image, f => f.Internet.Avatar());

                var users = userFaker.Generate(100).ToList();

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Passwor1+");
                }
            }
        }
    }
}