using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Learns.Domain.Abstractions
{
    public interface IDomainEventHandler<TDomainEvent> : INotificationHandler<TDomainEvent> where TDomainEvent : IDomainEvent
    {
        // 
        //Task Handle(TDomainEvent domainEvent, CancellationToken cancellationToken);

    }
}
