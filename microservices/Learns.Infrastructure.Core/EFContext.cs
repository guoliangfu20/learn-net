using DotNetCore.CAP;
using Learns.Infrastructure.Core.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Learns.Infrastructure.Core
{
    /// <summary>
    /// 借助 EFContext进行仓储层的管理
    /// 这里实现了工作单元；事务
    /// </summary>
    public class EFContext : DbContext, IUnitOfWork, ITransaction
    {
        protected IMediator _mediator;
        ICapPublisher _capBus;

        public EFContext(DbContextOptions options, IMediator mediator, ICapPublisher capBus) : base(options)
        {
            _mediator = mediator;
            _capBus = capBus;
        }

        #region IUnitOfWork

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);
            await _mediator.DispatchDomainEventsAsync(this);
            return true;
        }
        #endregion


        #region ITransaction

        private IDbContextTransaction _currentTransaction;
        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;
        public bool HasActiveTransaction => _currentTransaction != null;


        public Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;
            // 事件的存储与业务逻辑在同一个事务里
            _currentTransaction = Database.BeginTransaction(_capBus, autoCommit: false);
            return Task.FromResult(_currentTransaction);
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                // 释放事务
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }


        public void RollbackTransaction()
        {
            try
            {
                // 先判断是否为空
                // 不为空，则rollback
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
        #endregion
    }
}
