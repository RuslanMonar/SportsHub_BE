using System.Threading.Tasks;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Application.Services.Admin.Interfaces;
using Application;
using API.DTOs.Admin.Teams;
using System.IO;
using Microsoft.AspNetCore.Http;
using Application.Results.Admin.Teams;

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

        [HttpGet]
        [Route("categories/get")]
        public async Task<ActionResult<CategoriesResult>> GetAllCategories()
        {
            var result = await _teamsService.GetAllCategories();
            if (result.Success == false)
            {
                return BadRequest(result);
            }

            return Ok(result);
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

            if (result.Success == false)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("edit")]
        public async Task<ActionResult<Result>> EditTeam(EditTeamDto editTeamDto)
        {
            var editTeam = await _teamsService.EditTeam(editTeamDto.Id, editTeamDto.CurrentLocation, editTeamDto.NewLocation,
                editTeamDto.CurrentCategory, editTeamDto.NewCategory, editTeamDto.CurrentSubCategory, editTeamDto.NewSubCategory,
                editTeamDto.CurrentName, editTeamDto.NewName);
            if (editTeam.Success == false)
            {
                return BadRequest(editTeam.Errors);
            }

            return Ok(editTeam);
        }

        [HttpGet]
        [Route("GetAllTeams")]
        public async Task<ActionResult<AllTeamsResult>> GetAllTeams()
        {
            var result = await _teamsService.GetAllTeams();
            return Ok(result);
        }
    }
}