using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Application.Services.Admin.Interfaces
{
    public interface ITeamsService
    {
        Task<Result> AddTeam(string name, string location, int category, int subcategory, string imagePath);

    }
}