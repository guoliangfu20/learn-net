using System;
using System.Collections.Generic;
using System.Text;
using Learns.Domain;
using Learns.Domain.OrderAggregate;
using Learns.Infrastructure.Core;

namespace Learns.Infrastructure.Respositories
{
    public interface IOrderRepository : IRepository<Order, long>
    {

    }
}
