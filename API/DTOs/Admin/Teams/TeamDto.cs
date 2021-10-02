using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.DTOs.Admin.Teams
{
    public class TeamDto
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }

        public IFormFile Image { get; set; }
    }
}