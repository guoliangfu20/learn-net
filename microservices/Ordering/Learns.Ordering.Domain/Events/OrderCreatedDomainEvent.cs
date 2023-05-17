using Learns.Domain.Abstractions;
using Learns.Ordering.Domain.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learns.Ordering.Domain.Events
{
    public class OrderCreatedDomainEvent : IDomainEvent
    {
        public Order Order { get; private set; }

        public OrderCreatedDomainEvent(Order order) { Order = order; }
    }
}
