using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddIdentityCore<IdentityUser>()
                .AddEntityFrameworkStores<DataContext>()
                .AddSignInManager<SignInManager<IdentityUser>>();
            services.AddAuthentication();
            return services;
        }
    }
}