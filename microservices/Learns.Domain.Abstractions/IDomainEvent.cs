using MediatR;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Learns.Domain.Abstractions
{
    public interface IDomainEvent : INotification
    {
    }
}
