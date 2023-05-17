using System;
using System.Collections.Generic;
using System.Text;
using Learns.Domain;
using Learns.Ordering.Domain.OrderAggregate;
using Learns.Infrastructure.Core;

namespace Learns.Ordering.Infrastructure.Respositories
{
    public interface IOrderRepository : IRepository<Order, long>
    {

    }
}
