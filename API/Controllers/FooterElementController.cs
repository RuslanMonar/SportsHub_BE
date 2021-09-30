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
        public IActionResult GetFooter()
        {




            var users = _footerelements.GetAllitems();
            return Ok(new FooterElementDto { Elements = users});
                
           

            
        }
    }
}