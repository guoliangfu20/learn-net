using EasyCaching.Core;
using Learns.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;

namespace Learns.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        [ResponseCache(Duration = 6000, VaryByQueryKeys = new string[] { "query" })]
        public OrderModel GetOrder([FromQuery] string query)
        {
            return new OrderModel { Id = 100, Name = "100 订单商品", Date = DateTime.Now };
        }


        public IActionResult GetDis([FromServices] IDistributedCache cache, [FromServices] IEasyCachingProvider easyCaching, [FromQuery] string query)
        {
            #region IEasyCachingProvider

            string key = $"GetDis-{query ?? ""}";
            var time = easyCaching.Get(key, () => "guoliang_cache_" + DateTime.Now.ToString(), TimeSpan.FromSeconds(600));


            #endregion
            return Content("getdis: " + time);
        }

    }
}
