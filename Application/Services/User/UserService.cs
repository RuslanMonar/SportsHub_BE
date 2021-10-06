using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain;
using Microsoft.AspNetCore.Identity;
using Application.Services.EmailService;
using Microsoft.Extensions.Configuration;
using System;
using Application.Services.User;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class UserService: IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserAccessorService _userAccessorService;
        private readonly IConfiguration _config;
        private readonly IEmailSender _emailSender;
        public UserService(UserManager<AppUser> userManager, IConfiguration config, IUserAccessorService userAccessorService, IEmailSender emailSender)
        {
            _userManager = userManager;
            _config = config;
            _userAccessorService = userAccessorService;
            _emailSender = emailSender;
        }

        public async Task<Result> UpdateUserAsync(string name, string email, string image)
        {
            var user = await _userManager.FindByIdAsync(_userAccessorService.GetUserId());
            string[] fullname = name.Split(' ');
            string firstName = fullname[0];
            string lastName = fullname[1];
            if (user == null)
            {
                return new Result
                {
                    Success = false,
                    Errors = new[] { "User does not exists." }
                };
            }

            user.FirstName = firstName;
            user.LastName = lastName;
            user.Email = email;
            user.Image = image;
            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                return new Result
                {
                    Success = false,
                    Errors = updateResult.Errors.Select(x => x.Description)
                };
            }

            return new Result
            {
                Success = true,
                Errors = null
            };

        }


        public async Task<Result> ContactUsAsync(string firstName, string email, string phone, string usermessage)
        {

            Console.WriteLine(firstName+" " +email + " "+ phone + " " + usermessage);
            string userId = _userAccessorService.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return new Result
                {
                    Success = false,
                    Errors = new[] { "User does not exists." }
                };
            }
          
            var contactformTitle = "Contact US from " + email ;
            var contactformMessage = "Name:" + firstName + "\n" +" Phone:"+phone + "\n" + " Email: " + email + "\n" + " Message:" + usermessage;
            try
            {
                var message = new Message(email, contactformTitle, contactformMessage, null);
                await _emailSender.SendEmailAsync(message);
            }
            catch (Exception ex)
            {
                return new Result
                {
                    Errors = new[] { ex.Message },
                    Success = false
                };
            }
            return new Result
            {
                Errors = null,
                Success = true
            };

        }


        public async Task<UserDto> GetUserAsync()
        {

            var user = await _userManager.FindByIdAsync(_userAccessorService.GetUserId());
            bool isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            if (user == null)
            {
                throw new Exception("User does not exists");
            }

            return new UserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                IsAdmin = isAdmin
            };
        }
        
    }
}