using DotNetCore.CAP;
using Learns.Infrastructure.Core.Behaviors;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learns.Ordering.Infrastructure
{
    public class OrderingContextTransactionBehavior<TRequest, TResponse> : TransactionBehavior<OrderingContext, TRequest, TResponse>
    {
        public OrderingContextTransactionBehavior(OrderingContext dbContext, ICapPublisher capBus, ILogger<OrderingContextTransactionBehavior<TRequest, TResponse>> logger) : base(dbContext, capBus, logger)
        {
        }
    }
}
