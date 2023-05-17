using Learns.Ordering.Domain.OrderAggregate;
using Learns.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learns.Ordering.Infrastructure.Respositories
{
    public class OrderRepository : Repository<Order, long, OrderingContext>, IOrderRepository
    {
        public OrderRepository(OrderingContext context) : base(context)
        {

        }
    }
}
