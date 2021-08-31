using System.Threading.Tasks;
using Application.Faker;
using Data;
using Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            // add some data in db if it empty and create migration

            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<DataContext>();
            var userManager = services.GetRequiredService<UserManager<AppUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                var AdminRole = new IdentityRole("Admin");
                await roleManager.CreateAsync(AdminRole);
            }
            if (!await roleManager.RoleExistsAsync("User"))
            {
                var UserRole = new IdentityRole("User");
                await roleManager.CreateAsync(UserRole);
            }
            await context.Database.MigrateAsync();
            await Seed.SeedData(context, userManager, roleManager);
            await UsersFaker.GenerateUsers(userManager);
            

            await host.RunAsync();
        }
        
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
