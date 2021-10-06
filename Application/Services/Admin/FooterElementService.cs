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
using Application.Results.Admin.Users;
using static Application.Results.Admin.Users.FooterElementResultDto;

namespace Application.Services.Admin
{
    public class FooterElementService : IFooterElementsService
    {
        public DataContext _dataContext;

        public FooterElementService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        private FooterElementResultDto AddFooterElement(FooterElement footerElement)
        {
            return new FooterElementResultDto
            {
                Id = footerElement.Id,
                Name = footerElement.Name,
                Category = footerElement.Category,
                IsVisible = footerElement.IsVisible,
                IsDeletable = footerElement.IsDeletable,
                SourceLink = footerElement.SourceLink
            };
        }



        public FooterElementResult GetAllFooter()
        {
            var footers = new List<FooterElementResultDto>();
            return new FooterElementResult
            {
                Success = true,
                Elements = footers


            };
        }
    }
}
