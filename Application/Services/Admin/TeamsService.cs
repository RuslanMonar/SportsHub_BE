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
    public class TeamsService : ITeamsService
    {
        public DataContext _dataContext;

        public TeamsService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Result> AddTeam(string name, string location, string category, string subcategory, string imagePath)
        {


            Team team = new Team
            {
                Name = name,
                Location = location,
                ImageUrl = imagePath
            };


            await _dataContext.Teams.AddAsync(team);
            await _dataContext.SaveChangesAsync();
            

            return new Result
            {
                Success = true
            };
        }
    }
}