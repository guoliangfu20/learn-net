using EFCoreLearn.MyContext;
using EFCoreLearn.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace EFCoreLearn.Controllers
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
        public bool Add([FromServices] MyDbContext myDbContext)
        {
            _logger.LogDebug("get ��ʼ���ѧԺ��������������.....");

            var student = myDbContext.Students.Add(new Student { Name = "����", Gender = false });

            myDbContext.SaveChanges();
            return student != null;
        }

        [HttpPost(Name = "addSql")]
        public bool AddSql([FromServices] MySqlDbContext dbContext)
        {
            dbContext.Students.Add(new Student { Name = "����", Gender = true });
            dbContext.SaveChanges();
            return true;
        }

    }
}