using Microsoft.AspNetCore.Mvc;

namespace WeatherService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserWeatherController : ControllerBase
    {
        [HttpPost]
        public IActionResult PostLocalWeather()
        {
            // Accept a model and validate it, good for testing scenario            

            return Ok();
        }
    }
}
