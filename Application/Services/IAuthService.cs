using System.Threading.Tasks;

namespace Application.Services
{
    public interface IAuthService
    {
        Task<AuthResult> RegisterAsync(string email, string password, string username, string firstName, string LastName);
        Task<AuthResult> LoginAsync(string email, string password);
    }
}