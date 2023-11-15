using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTDemo.Controllers
{
   /* The `[ApiController]` attribute is used to indicate that the controller is an API controller. It
   provides some default behaviors for API controllers, such as automatic model validation and
   response formatting. */
    [ApiController]
    [Route("[controller]")]
    /* The WeatherForecastController class is responsible for providing weather summaries. */
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

       /* The line `private readonly ILogger<WeatherForecastController> _logger;` is declaring a
       private readonly field named `_logger` of type `ILogger<WeatherForecastController>`. */
        private readonly ILogger<WeatherForecastController> _logger;

       /* The `public WeatherForecastController(ILogger<WeatherForecastController> logger)` is a
       constructor for the `WeatherForecastController` class. It takes an argument of type
       `ILogger<WeatherForecastController>` named `logger`. */
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }
       /// <summary>
       /// This C# function returns an array of weather forecasts for the next 5 days.
       /// </summary>
       /// <returns>
       /// The method is returning an IEnumerable of WeatherForecast objects.
       /// </returns>
        [Authorize]
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}