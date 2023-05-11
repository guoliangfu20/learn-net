using Learns.Domain.OrderAggregate;
using Learns.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learns.Infrastructure.Respositories
{
    public class OrderRepository : Repository<Order, long, DomainContext>, IOrderRepository
    {
        public OrderRepository(DomainContext context) : base(context)
        {

        }
    }
}
