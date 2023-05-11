using MediatR;
using System.Collections.Generic;

namespace Learns.API.Application.Queries
{
    public class MyOrderQuery : IRequest<List<string>>
    {
        public string UserName { get; private set; }
    }
}
