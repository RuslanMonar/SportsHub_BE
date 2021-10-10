using API.DTOs;
using Application.Services.User;
using Domain;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IUserService
    {
        Task<Result> UpdateUserAsync(string name, string email, string image);

        Task<Result> ContactUsAsync(string firstName,string email,string phone, string message);

        Task<UserDto> GetUserAsync();
        Task<UserImageDto> GetUserImageASync();
        
    }
}
