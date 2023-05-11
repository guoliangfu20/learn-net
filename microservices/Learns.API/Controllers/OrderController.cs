using Learns.API.Application.Commands;
using Learns.API.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learns.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<long> CreateOrder([FromBody] CreateOrderCommand orderCommand)
        {
            return await _mediator.Send(orderCommand, HttpContext.RequestAborted);
        }


        [HttpGet]
        public async Task<List<string>> OrderQuery([FromBody] MyOrderQuery myOrderQuery) => await _mediator.Send(myOrderQuery);
    }
}
