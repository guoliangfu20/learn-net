﻿using DotNetCore.CAP;
using Learns.Infrastructure.Core.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Learns.Infrastructure.Core.Behaviors
{
    /// <summary>
    /// 事务管理
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class TransactionBehavior<TDbContext, TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TDbContext : EFContext
    {

        ILogger _logger;
        TDbContext _dbContext;
        ICapPublisher _capBus;
        public TransactionBehavior(TDbContext dbContext, ICapPublisher capBus, ILogger logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _capBus = capBus ?? throw new ArgumentNullException(nameof(capBus));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = default(TResponse);
            var typeName = request.GetGenericTypeName();

            try
            {
                // 判断的含义：
                // 如果事务开启，则继续执行后面的逻辑
                // 如果事务未开启，下面代码尝试开启事务，并执行
                if (_dbContext.HasActiveTransaction)
                {
                    return await next();
                }

                var strategy = _dbContext.Database.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    Guid transactionId;
                    using (var transaction = await _dbContext.BeginTransactionAsync())
                    using (_logger.BeginScope("TransactionContext:{TransactionId}", transaction.TransactionId))
                    {
                        _logger.LogInformation("----- 开始事务 {TransactionId} ({@Command})", transaction.TransactionId, typeName, request);

                        response = await next();  // 后续操作(类似中间件的操作)

                        _logger.LogInformation("----- 提交事务 {TransactionId} {CommandName}", transaction.TransactionId, typeName);

                        await _dbContext.CommitTransactionAsync(transaction);

                        transactionId = transaction.TransactionId;
                    }
                });

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "处理事务出错 {CommandName} ({@Command})", typeName, request);

                throw;
            }
        }
    }
}
