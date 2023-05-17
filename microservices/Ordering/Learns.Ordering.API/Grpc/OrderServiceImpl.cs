using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Learns.Ordering.API.Grpc
{
    public class OrderServiceImpl : OrderService.OrderServiceBase
    {
        IMediator _mediator;

        ILogger _logger;

        public OrderServiceImpl(IMediator mediator, ILogger<OrderServiceImpl> logger)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public override async Task<Int64Value> CreateOrder(CreateOrderCommand request, ServerCallContext context)
        {
            var cmd = new Learns.Ordering.API.Commands.CreateOrderCommand(request.ItemCount);
            var res = await _mediator.Send(cmd);
            return new Int64Value { Value = res };
        }

        public override async Task<SearchResponse> Search(SearchRequest request, ServerCallContext context)
        {
            var query = new Learns.Ordering.API.Application.Queries.MyOrderQuery { UserName = request.UserName };
            var r = await _mediator.Send(query);
            var response = new SearchResponse();
            response.Orders.AddRange(r);
            return response;
        }
    }
}
