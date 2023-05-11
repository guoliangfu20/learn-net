using DotNetCore.CAP;
using Learns.Infrastructure.Core.Behaviors;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learns.Infrastructure
{
    /// <summary>
    /// 事务处理对象
    /// （手动处理事务）
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class DomainContextTransactionBehavior<TRequest, TResponse> : TransactionBehavior<DomainContext, TRequest, TResponse>
    {
        public DomainContextTransactionBehavior(DomainContext dbContext, ICapPublisher capBus,
            ILogger<DomainContextTransactionBehavior<TRequest, TResponse>> logger) : base(dbContext, capBus, logger)
        {

        }
    }
}
