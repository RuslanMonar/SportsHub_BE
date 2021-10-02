using Domain;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IUserService
    {
        Task<Result> UpdateUserAsync(string firstName, string lastName, string email);

        Task<Result> ContactUsAsync(string firstName,string email,string phone, string message);

        Task<AppUser> GetUserAsync();
        
    }
}
