using Domain;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IAuthService
    {
        Task<AuthResult> RegisterAsync(string email, string password, string firstName, string LastName);
        Task<AuthResult> LoginAsync(string email, string password);
        Task<ChangePasswordResult> ChangePasswordAsync(string currentPassword, string newPassword);
        Task<AuthResult> LoginWithFacebookAsync(string accessToken);


        Task<AuthResult> AuthWithGoogleAsync(string email, string id, string firstName, string LastName, string imageUrl);

    }
}