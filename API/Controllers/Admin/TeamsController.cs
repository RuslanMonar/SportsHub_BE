using System.Threading.Tasks;
using API.DTOs.Admin.Teams;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Application.Services.Admin.Interfaces;
using System;
using Application;
using Microsoft.AspNetCore.Identity;
using Domain;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace API.Controllers.Admin
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamsService _teamsService;

        public TeamsController(ITeamsService teamsService)
        {
            _teamsService = teamsService;

        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult<Result>> AddTeam(TeamDto team)
        {
            
            if (team == null)
            {
                return BadRequest(new Result
                {
                    Errors = new[] { $"Incorrect data was sent." }
                });
            }

            // save logo in folders
            string path = "/resources/img/teams/" + $"{team.Name}-{team.Location}.png";
            using (var fileStream = new FileStream(Directory.GetCurrentDirectory() + path, FileMode.Create))
            {
                await team.Image.CopyToAsync(fileStream);
            }


            var result = await _teamsService.AddTeam(
                team.Name,
                team.Location,
                team.Category,
                team.SubCategory,
                path
                );

            return Ok(result);
        }

    }
}