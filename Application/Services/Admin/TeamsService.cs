using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Services.Admin.Interfaces;
using Domain;
using Data;
using Application.Results.Admin.Teams;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Application.Services.Admin
{
    public class TeamsService : ITeamsService
    {
        public DataContext _dataContext;

        public TeamsService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }



        public async Task<CategoriesResult> GetAllCategories()
        {
            var categories = _dataContext.Categories;
            var subCategories = _dataContext.SubCategories;

            CategoriesResult categoriesResult = new CategoriesResult();

            await Task.Run(() => {
                foreach (Category category in categories)
                {
                    categoriesResult.categories.Add(new CategoryDto
                    {
                        Id = category.Id,
                        Name = category.Name
                    });
                };
                foreach (SubCategory sub in subCategories)
                {
                    categoriesResult.subCategories.Add(new SubCategoryDto
                    {
                        Id = sub.Id,
                        Name = sub.Name,
                        CategoryId = sub.CategoryId
                    });
                }
            });
            

            categoriesResult.Success = true;
            return categoriesResult;
        }

        public async Task<Result> AddTeam(string name, string location, int categoryID, int subCategoryID, string imagePath)
        {
            Category category = await _dataContext.Categories.FindAsync(categoryID);

            if (category == null)
            {
                return new Result
                {
                    Success = false,
                    Errors = new[] { "There is no such category" }
                };
            }


            SubCategory subCategory = await _dataContext.SubCategories.FindAsync(subCategoryID);
            if (subCategory == null)
            {
                return new Result
                {
                    Success = false,
                    Errors = new[] { "There is no such subcategory" }
                };
            }

            Team team = new Team
            {
                Name = name,
                Location = location,
                Category = category,
                SubCategory = subCategory,
                ImageUrl = imagePath,
                AddedAt = DateTime.Now
            };

            try
            {
                await _dataContext.Teams.AddAsync(team);
                await _dataContext.SaveChangesAsync();
            }
            catch(Exception exc)
            {
                return new Result
                {
                    Success = false,
                    Errors = new[] { exc.Message }
                };
            }
            

            return new Result
            {
                Success = true
            };
        }

        public async Task<AllTeamsResult> GetAllTeams()
        {
            var allTeams = _dataContext.Teams;
            AllTeamsResult teams = new AllTeamsResult();
            await Task.Run(() =>
            {
                foreach (var team in allTeams)
                {
                    teams.Teams.Add(new GetAllTeamsDTO
                    {
                        Id = team.Id,
                        TeamName = team.Name,
                        Location = team.Location,
                        AddedAt = team.AddedAt.ToString("dd'/'MM'/'yyyy"),
                        CategoryID = team.CategoryId,
                        CategoryName = _dataContext.Categories.Find(team.CategoryId).Name,
                        SubCategoryID = team.SubCategoryId,
                        SubCategoryName = _dataContext.SubCategories.Find(team.SubCategoryId).Name,
                    });
                }
            });
            return teams;
        }

        public async Task<Result> EditTeam(int id, string currentLocation, string? newLocation, string currentCategory, string? newCategory,
            string currentSubCategory, string? newSubCategory, string currentName, string? newName)
        {
            var team = await _dataContext.Teams.FindAsync(id);
            if (team == null)
            {
                return new Result
                {
                    Errors = new[] { "Team does not exist" },

                    Success = false
                };
            }
            if (newLocation != null)
            {
                if(newLocation == currentLocation)
                {
                    return new Result
                    {
                        Errors = new[] { "The new location must be different from the old one" },

                        Success = false
                    };
                }
                team.Location = newLocation;
            }
            if (newCategory != null)
            {
                if (newCategory == currentCategory)
                {
                    return new Result
                    {
                        Errors = new[] { "The new category must be different from the old one" },

                        Success = false
                    };
                }
                bool flag = true;
                foreach(var category in _dataContext.Categories)
                {
                    if(newCategory == category.Name)
                    {
                        team.CategoryId = category.Id;
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    return new Result
                    {
                        Errors = new[] { "There is no such a category" },
                        Success = false
                    };
                }      
            }
            if (newSubCategory != null)
            {
                if (newSubCategory == currentSubCategory)
                {
                    return new Result
                    {
                        Errors = new[] { "The new subcategory must be different from the old one" },

                        Success = false
                    };
                }
                bool flag = true;
                foreach (var subCategory in _dataContext.SubCategories)
                {
                    if (newSubCategory == subCategory.Name)
                    {
                        team.SubCategoryId = subCategory.Id;
                        break;
                    }
                }
                if (flag)
                {
                    return new Result
                    {
                        Errors = new[] { "There is no such a subcategory" },
                        Success = false
                    };
                }  
            }
            if (newName != null)
            {
                if (newName == currentName)
                {
                    return new Result
                    {
                        Errors = new[] { "The new name must be different from the old one" },

                        Success = false
                    };
                }
                team.Name = newName;
            }
            await _dataContext.SaveChangesAsync();
            return new Result
            {
                Success = true,
                Errors = null
            };

        }
    }
}