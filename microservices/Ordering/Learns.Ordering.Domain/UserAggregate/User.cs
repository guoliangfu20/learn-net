using Learns.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learns.Ordering.Domain.UserAggregate
{
    public class User : Entity<int>, IAggregateRoot
    {
        public User()
        {

        }
    }
}
