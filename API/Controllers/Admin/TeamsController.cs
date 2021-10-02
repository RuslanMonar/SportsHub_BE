using System.Threading.Tasks;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Application.Services.Admin.Interfaces;
using Application;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace API.Controllers.Admin
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Roles = "Admin")]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamsService _teamsService;

        public TeamsController(ITeamsService teamsService)
        {
            _teamsService = teamsService;

        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult<Result>> AddTeam(IFormCollection data, IFormFile image)
        {
            
            if (data == null)
            {
                return BadRequest(new Result
                {
                    Errors = new[] { $"Incorrect data was sent." }
                });
            }

            // save logo in folders
            string path = "/resources/img/teams/" + $"{data["Name"]}-{data["Location"]}.png";
            using (var fileStream = new FileStream(Directory.GetCurrentDirectory() + path, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }


            var result = await _teamsService.AddTeam(
                data["Name"],
                data["Location"],
                int.Parse(data["CategoryId"]),
                int.Parse(data["SubCategoryId"]),
                path
                );

            return Ok(result);
        }

    }
}