using Domain;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IUserService
    {
        Task<Result> UpdateUserAsync(string firstName, string lastName, string email);

        Task<AppUser> GetUserAsync();
        
    }
}
