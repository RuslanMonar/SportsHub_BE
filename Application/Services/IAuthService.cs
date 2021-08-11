using System.Threading.Tasks;

namespace Application.Services
{
    public interface IAuthService
    {
        Task<AuthResult> RegisterAsync(string email, string password, string firstName, string LastName);
        Task<AuthResult> LoginAsync(string email, string password);
        Task<ChangePasswordResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
    }
}