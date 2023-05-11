using Learns.Domain.UserAggregate;
using Learns.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learns.Infrastructure.Respositories
{
    public class UserRepository : Repository<User, int, DomainContext>, IUserRespository
    {
        public UserRepository(DomainContext context) : base(context)
        {

        }
    }
}
