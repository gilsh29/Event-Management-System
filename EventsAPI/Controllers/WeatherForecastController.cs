using EventsData.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace EventsAPI.Controllers
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
        private readonly IMemoryCache _cache;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMemoryCache cache)
        {
            _logger = logger;
            _cache = cache;
        }
        [HttpGet("event/{id}/weather")]
        public async Task<IActionResult> GetEventWeather(int id, [FromServices] IEventRepository eventRepo, [FromServices] WeatherService weatherService)
        {
            var ev = eventRepo.GetEventById(id);
            if (ev == null)
                return NotFound($"Event with ID {id} not found.");

            var weather = await weatherService.GetWeatherAsync(ev.Location);
            if (weather == null)
                return StatusCode(503, "Weather service unavailable or failed.");

            return Ok(weather);
        }

  
    }

}
