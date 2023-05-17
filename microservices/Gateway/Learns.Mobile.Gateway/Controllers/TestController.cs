using Microsoft.AspNetCore.Mvc;

namespace Learns.Mobile.Gateway.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetTest()
        {
            return Content("Learns.Mobile.Gateway");
        }
    }
}
