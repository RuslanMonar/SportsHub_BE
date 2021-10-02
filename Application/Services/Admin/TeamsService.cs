using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Admin.Users;
using Application.Services.Admin.Interfaces;
using Domain;
using Data;

namespace Application.Services.Admin
{
    public class TeamsService : ITeamsService
    {
        public DataContext _dataContext;

        public TeamsService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Result> AddTeam(string name, string location, int categoryID, int subCategoryID, string imagePath)
        {
            Category category = await _dataContext.Categories.FindAsync(categoryID);
            SubCategory subCategory = await _dataContext.SubCategories.FindAsync(subCategoryID);

            Team team = new Team
            {
                Name = name,
                Location = location,
                Category = category,
                SubCategory = subCategory,
                ImageUrl = imagePath,
                AddedAt = DateTime.Now
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