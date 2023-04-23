using DependencyInjectionScopeAndDisposableDemo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionScopeAndDisposableDemo.Controllers
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

        //[HttpGet]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    var rng = new Random();
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = rng.Next(-20, 55),
        //        Summary = Summaries[rng.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}

        [HttpGet]
        public int Get([FromServices] IOrderService orderService1,
            [FromServices] IOrderService orderService2,
            [FromServices] IHostApplicationLifetime hostApplicationLifetime,
            [FromQuery] bool stop = false)
        {

            //Console.WriteLine("=======1==========");
            //using (IServiceScope scope = HttpContext.RequestServices.CreateScope())
            //{
            //    var service = scope.ServiceProvider.GetService<IOrderService>();
            //}
            //Console.WriteLine("=======2==========");


            if (stop)
            {
                hostApplicationLifetime.StopApplication();
            }



            Console.WriteLine("请求结束");
            return 1;
        }

    }
}
