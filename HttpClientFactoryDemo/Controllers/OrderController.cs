using HttpClientFactoryDemo.Clients;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HttpClientFactoryDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        OrderServiceFactoryClient _orderServiceClient;
        public OrderController(OrderServiceFactoryClient orderServiceClient)
        {
            _orderServiceClient = orderServiceClient;
        }


        [HttpGet("Get")]
        public async Task<string> Get()
        {
            return await _orderServiceClient.Get();
        }

        [HttpGet("NamedGet")]
        public async Task<string> NamedGet([FromServices] NamedOrderServiceClient serviceClient)
        {
            return await serviceClient.Get();
        }


        [HttpGet("TypedGet")]
        public async Task<string> TypedGet([FromServices] TypedOrderServiceClient typedOrderServiceClient)
        {
            return await typedOrderServiceClient.Get();
        }
    }
}
