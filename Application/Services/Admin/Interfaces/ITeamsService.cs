﻿using Application.Results.Admin.Teams;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Application.Services.Admin.Interfaces
{
    public interface ITeamsService


    {
        Task<CategoriesResult> GetAllCategories();
        Task<Result> AddTeam(string name, string location, int category, int subcategory, string imagePath);
        Task<Result> EditTeam(int id, string currentLocation, string? newLocation, string currentCategory, string? newCategory,
            string currentSubCategory, string? newSubCategory, string currentName, string? newName);

        Task<AllTeamsResult> GetAllTeams();
    }
}