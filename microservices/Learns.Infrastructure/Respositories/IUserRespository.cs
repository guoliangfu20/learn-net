using Learns.Domain.UserAggregate;
using Learns.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learns.Infrastructure.Respositories
{
    public interface IUserRespository : IRepository<User, int>
    {

    }
}
