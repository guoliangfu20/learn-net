using DotNetCore.CAP;
using Learns.Domain.OrderAggregate;
using Learns.Infrastructure.Respositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Learns.API.Application.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, long>
    {
        IOrderRepository _orderRepository;
        ICapPublisher _capBus;
        public CreateOrderCommandHandler(IOrderRepository orderRepository, ICapPublisher capBus)
        {
            _orderRepository = orderRepository;
            _capBus = capBus;
        }

        public async Task<long> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {

            var address = new Address("wen san lu", "hangzhou", "310000");
            var order = new Order("xiaohong1999", "xiaohong", 25, address);

            _orderRepository.Add(order);
            await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return order.Id;
        }
    }
}
