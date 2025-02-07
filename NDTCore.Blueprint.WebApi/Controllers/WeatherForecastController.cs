using Microsoft.AspNetCore.Mvc;
using NDTCore.Blueprint.Stack.Kafka;

namespace NDTCore.Blueprint.WebApi.Controllers
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
        private readonly KafkaServiceExample _kafkaServiceExample;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, KafkaServiceExample kafkaServiceExample)
        {
            _logger = logger;
            _kafkaServiceExample = kafkaServiceExample;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            _kafkaServiceExample.SendMessage();

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
