using System.Configuration;

using API.Extensions;
using Application.FacebookResult;
using Application.Services;
using Application.Services.Admin;
using Application.Services.Admin.Interfaces;
using Application.Services.EmailService;
using Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using ConfigurationSection = System.Configuration.ConfigurationSection;
using Domain;

namespace API
{
    public class Startup
    {

        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;

        }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddDbContext<DataContext>(opt => opt.UseSqlServer(_config.GetConnectionString("DefaultConnection")));
            services.AddIdentityServices(_config);
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUsersService , UsersService>();
            services.AddScoped<ITeamsService, TeamsService>();

            // facebook auth install
            var facebookAuthSettings = new FacebookAuthSettings();
            _config.Bind(nameof(FacebookAuthSettings), facebookAuthSettings);
            services.AddSingleton(facebookAuthSettings);
            services.AddHttpClient();
            services.AddSingleton<IFacebookAuthService, FacebookAuthService>();
            services.AddScoped<IUserAccessorService, UserAccessorService>();


           


            // email service config

            var emailConfig = _config
                .GetSection("EmailConfig")
                .Get<EmailConfig>();
            services.AddSingleton(emailConfig);

            services.AddScoped<IEmailSender, EmailSender>();


            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });

            services.AddCors(Opt =>
            {
                Opt.AddPolicy("CorsPolicy", policy =>
                 {
                     policy.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();//.WithOrigins("http://localhost:3000");
                 });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }
            
            

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
