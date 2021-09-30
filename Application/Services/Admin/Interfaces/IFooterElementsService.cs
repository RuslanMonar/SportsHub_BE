using Domain;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Admin.Interfaces
{
    public interface IFooterElementsService
    {
        Task<Result> AddFooterElement(string name,string category, bool isVisible, bool isDeletable, string SourceLink);
        public  IEnumerable<FooterElement> GetAllitems();


    }
}