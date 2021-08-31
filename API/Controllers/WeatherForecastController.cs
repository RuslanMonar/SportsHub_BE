using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services.EmailService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IEmailSender _emailSender;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IEmailSender emailSender)
        {
            _emailSender = emailSender;
            _logger = logger;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("testemail")]
        public async Task<IActionResult> TestSendMail()
        {
          
            var message = new Message(new string[] { "boyyou481@gmail.com" }, "Test email", "HELLO HTML.", null);
            await _emailSender.SendEmailAsync(message);
            return Ok();
       
        }
    }
}
