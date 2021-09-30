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
    public class FooterElementService : IFooterElementsService
    {
        public DataContext _dataContext;

        public FooterElementService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Result> AddFooterElement(string name, string category, bool isVisible, bool isDeletable, string SourceLink)
        {
            FooterElement footerElement = new FooterElement
            {
                Name = name,
                Category = category,
                IsVisible = isVisible,
                IsDeletable = isDeletable,
                SourceLink = SourceLink


            };
            await _dataContext.FooterElements.AddAsync(footerElement);
            await _dataContext.SaveChangesAsync();

            return new Result
            {
                Success = true
            };
        }

        public  IEnumerable<FooterElement> GetAllitems()
        {
            return _dataContext.FooterElements.ToList();
        }
    }
}
