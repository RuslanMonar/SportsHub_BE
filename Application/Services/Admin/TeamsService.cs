using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Admin.Users;
using Application.Services.Admin.Interfaces;
using Domain;
using Data;
using Application.Results.Admin.Teams;

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
    }
}