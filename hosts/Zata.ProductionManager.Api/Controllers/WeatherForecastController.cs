using Microsoft.AspNetCore.Mvc;
using Zata.ProductionManager.Domain.Repositories.EfCore;

namespace Zata.ProductionManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IProductEfCoreRepository _productEfCore;

        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IProductEfCoreRepository productEfCore)
        {
            _logger = logger;
            _productEfCore = productEfCore;
        }

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

        [HttpPost]
        public async Task<IActionResult> InsertDemoProductionAsync([FromQuery] string name)
        {
            var production = new Domain.Entities.Product();
            production.Name = name;

            await using var repo = _productEfCore;
            var inserted = await repo.InsertAsync(production);

            return Ok(inserted);
        }
    }
}