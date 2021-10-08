using System.Threading.Tasks;
using API.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using Application;
using Microsoft.AspNetCore.Identity;
using Domain;
using Application.Services.Admin.Interfaces;
using Application.Results.Admin.Users;

namespace API.Controllers.Admin
{
    [ApiController]
    [Route("api/[controller]")]
    public class FooterElementController : ControllerBase
    {
        private readonly IFooterElementsService _footerelements;

        public FooterElementController(IFooterElementsService footerelement)
        {
            _footerelements = footerelement;

        }

        [HttpGet]
        [Route("get")]
        public ActionResult<FooterElementResultDto> GetFooter()
        {

            try
            {
                var result = _footerelements.GetAllFooter();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }




        }
    }
}