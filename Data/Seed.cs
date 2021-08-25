using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace Data
{
    public class Seed
    {
        public static async Task SeedData(DataContext context, UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
                var admin = new AppUser
                    {UserName = "AdminUN", Email = "admin@test.com", FirstName = "FAdmin", LastName = "LAdmin"};
                await userManager.CreateAsync(admin, "PassworD1!");
                await userManager.AddToRoleAsync(admin, "Admin");
            }

            if (await roleManager.RoleExistsAsync("Admin") &&
                !userManager.Users.Any(u => u.Email == "admin@test.com" && u.UserName == "AdminUN"))
            {
                var admin = new AppUser
                    {UserName = "AdminUN", Email = "admin@test.com", FirstName = "FAdmin", LastName = "LAdmin"};
                await userManager.CreateAsync(admin, "PassworD1!");
                await userManager.AddToRoleAsync(admin, "Admin");
            }

            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser {UserName = "bob", Email = "bob@test.com", FirstName = "Bob", LastName = "BobBob"},
                    new AppUser {UserName = "tom", Email = "tom@test.com", FirstName = "tom", LastName = "tomtom"},
                    new AppUser {UserName = "jane", Email = "jane@test.com", FirstName = "jane", LastName = "janejane"},
                    new AppUser {UserName = "Mark", Email = "Mark@test.com", FirstName = "Mark", LastName = "MarkMark"},
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "PassworD1!");
                }
            }
        }
    }
}