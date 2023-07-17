using Microsoft.AspNetCore.Mvc;

namespace todoDockers.Controllers
{
    public class TodoController : Controller
    {
        public IActionResult Index()
        {
            ViewData["oneName"] = "GuoliangFu";
            return View();
        }
    }
}
