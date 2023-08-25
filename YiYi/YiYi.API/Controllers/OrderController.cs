using Microsoft.AspNetCore.Mvc;
using YiYi.Core.Controllers;
using YiYi.Entity.AttributeManager;

namespace YiYi.API.Controllers
{
    [Route("api/order")]
    [PermissionTable(Name = "order")]
    public class OrderController : Controller //: ApiBaseController<>
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
