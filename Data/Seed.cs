using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Data
{
    public class Seed
    {
        public static async Task SeedData(DataContext context, UserManager<IdentityUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<IdentityUser>
                {
                    new IdentityUser{ UserName = "bob", Email = "bob@test.com"},
                    new IdentityUser{ UserName = "tom", Email = "tom@test.com"},
                    new IdentityUser{ UserName = "jane", Email = "jane@test.com"},
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "PassworD1!");
                }
            }

        }
    }
}