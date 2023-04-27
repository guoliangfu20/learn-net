using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;

namespace RoutingDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OrderController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">必须可以转为long</param>
        /// <returns></returns>
        [HttpGet("{id:isGuoLong}")]
        public bool OrderExist([FromRoute] string id)
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">最大20</param>
        /// <param name="linkGenerator"></param>
        /// <returns></returns>
        [HttpGet("{id:max(20)}")]
        public bool Max([FromRoute] long id, [FromServices] LinkGenerator linkGenerator)
        {

            var a = linkGenerator.GetPathByAction(HttpContext,
                action: "Reque",
                controller: "Order",
                values: new { name = "abc" });

            var uri = linkGenerator.GetUriByAction(HttpContext,
                action: "Reque",
                controller: "Order",
                values: new { name = "abc" });
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ss">必填</param>
        /// <returns></returns>
        [HttpGet("{name:required}")]
        [Obsolete]
        public bool Reque(string name)
        {
            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="number">以三个数字开始</param>
        /// <returns></returns>
        [HttpGet("{number:regex(^\\d{{3}}$)}")]
        public bool Number(string number)
        {
            return true;
        }
    }
}
