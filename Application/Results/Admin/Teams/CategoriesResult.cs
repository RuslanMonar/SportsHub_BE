using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Results.Admin.Teams
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class SubCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CategoryId { get; set; }

    }

    public class CategoriesResult: Result
    {
        public List<CategoryDto> categories { get; set; }
        public List<SubCategoryDto> subCategories { get; set; }

        public CategoriesResult()
        {
            categories = new List<CategoryDto>();
            subCategories = new List<SubCategoryDto>();
        }
    }
}
