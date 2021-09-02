using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Admin.Users;
using Domain;

namespace Application.Services.Admin.Interfaces
{
    public interface IUsersService
    {
        Task<SearchResult>  SearchUserAsync(string Username);
        Task<Result> SwitchRolesAsync(string id);
    }
}