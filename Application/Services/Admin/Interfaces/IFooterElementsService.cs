using Domain;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Application.Results.Admin.Users.FooterElementResultDto;

namespace Application.Services.Admin.Interfaces
{
    public interface IFooterElementsService
    {

        FooterElementResult GetAllFooter();


    }
}