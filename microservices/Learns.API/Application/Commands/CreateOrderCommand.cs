using MediatR;

namespace Learns.API.Application.Commands
{
    public class CreateOrderCommand : IRequest<long>
    {
        public CreateOrderCommand(long itemCount)
        {
            ItemCount = itemCount;
        }
        public long ItemCount { get; private set; }
    }
}
