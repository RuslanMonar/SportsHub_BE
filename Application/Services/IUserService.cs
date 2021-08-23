using Domain;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IUserService
    {
        Task<IResult> UpdateUserAsync(string id, string firstName, string lastName, string email, string imageUrl);
    }
}
