using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Admin.Interfaces
{
    public interface IFooterArticleService
    {
        Task<Result> AddFooterArticle(string name, string text);
    }
}
