using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Admin.Users;
using Application.Services.Admin.Interfaces;
using Domain;
using Data;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Application.Services.Admin
{
    public class FooterArticleService : IFooterArticleService
    {
        public DataContext _dataContext;

        public FooterArticleService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Result> AddFooterArticle(string name, string text)
        {
            FooterArticle footerarticle = new FooterArticle
            {
                Name = name,
                Text = text
               


            };
            await _dataContext.FooterArticles.AddAsync(footerarticle);
            await _dataContext.SaveChangesAsync();

            return new Result
            {
                Success = true
            };
        }
        public IEnumerable<FooterArticle> GetAllItems()
        {
            return _dataContext.FooterArticles.ToList();
        }
    }
}
