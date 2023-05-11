using Learns.Domain.Abstractions;
using Learns.Domain.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learns.Domain.Events
{
    /// <summary>
    /// 领域事件定义在领域模型了
    /// </summary>
    public class OrderCreatedDomainEvent : IDomainEvent
    {
        public Order Order { get; private set; }
        public OrderCreatedDomainEvent(Order order)
        {
            this.Order = order;
        }
    }
}
