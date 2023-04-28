using Microsoft.AspNetCore.Mvc;

namespace Learns.API.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
