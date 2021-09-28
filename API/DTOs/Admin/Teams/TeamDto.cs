using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.DTOs.Admin.Teams
{
    public class TeamDto
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }

        public IFormFile Image { get; set; }
    }
}