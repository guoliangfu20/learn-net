using DotNetCore.CAP;
using Learns.Ordering.API.Application.IntegrationsEvents;
using Learns.Domain.Abstractions;
using Learns.Ordering.Domain.Events;
using System.Threading;
using System.Threading.Tasks;


namespace Learns.Ordering.API.DomainEventHandlers
{
    public class OrderCreatedDomainEventHandler : IDomainEventHandler<OrderCreatedDomainEvent>
    {
        ICapPublisher _capBus;
        public OrderCreatedDomainEventHandler(ICapPublisher capBus)
        {
            _capBus = capBus;
        }

        public async Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            await _capBus.PublishAsync("OrderCreated", new OrderCreatedIntegrationEvent(notification.Order.Id));
        }
    }
}
