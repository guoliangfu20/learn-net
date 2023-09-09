using EFCoreSqlLearn.MyDbContext;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreSqlLearn.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
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

        [HttpPost(Name = "add")]
        public bool Add([FromServices] MySqlDbContext dbContext)
        {

            _logger.LogDebug("-----   开始记录日志了琳琅了了了琳琅了了。。     --------");


            dbContext.orders.Add(new Model.Order { OrderName = "Iphone 16", OrderAddress = "北京海淀", Price = 10001, Total = 2, TotalPrice = 20002 });
            dbContext.SaveChanges();
            return true;
        }

    }
}