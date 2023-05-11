using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace MediatorDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var service = new ServiceCollection();

            service.AddMediatR(typeof(Program).Assembly);

            var serviceProvider = service.BuildServiceProvider();

            var mediator = serviceProvider.GetService<IMediator>();


            mediator.Send(new MyCommand { CommandName = "Command111" });

            Console.WriteLine("hello world");
        }
    }

    internal class MyCommand : IRequest<long>
    {
        public string CommandName { get; set; }
    }


    internal class MyCommandHandlerV2 : IRequestHandler<MyCommand, long>
    {
        public Task<long> Handle(MyCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"MyCommandHandlerV2 执行命令：{request.CommandName}");
            return Task.FromResult(10L);
        }
    }

    internal class MyCommandHandler : IRequestHandler<MyCommand, long>
    {
        public Task<long> Handle(MyCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"MyCommandHandler执行命令：{request.CommandName}");
            return Task.FromResult(10L);
        }
    }

    
}
