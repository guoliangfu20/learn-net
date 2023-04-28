using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learns.Domain.Abstractions
{
    public interface IDomainEventHandler<TDomainEvent> : INotificationHandler<TDomainEvent> where TDomainEvent : IDomainEvent
    {
    }
}
