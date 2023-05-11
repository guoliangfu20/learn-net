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


            //mediator.Send(new MyCommand { CommandName = "Command111" });

            //mediator.Publish(new MyEvent { EventName = "event..." });

            Console.WriteLine("hello world");
        }
    }

    #region IRequest IRequestHandler
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
    #endregion


    #region INotification INotificationHandler
    internal class MyEvent : INotification
    {
        public string EventName { get; set; }
    }

    internal class MyEventHandler : INotificationHandler<MyEvent>
    {
        public Task Handle(MyEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"MyEventHandler执行：{notification.EventName}");
            return Task.CompletedTask;
        }
    }

    internal class MyEventHandlerV2 : INotificationHandler<MyEvent>
    {
        public Task Handle(MyEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"MyEventHandler V2 执行：{notification.EventName}");
            return Task.CompletedTask;
        }
    }
    #endregion

}
