using Learns.API.Application.IntegrationsEvents;
using Learns.Domain.OrderAggregate;
using Learns.Infrastructure;
using Learns.Infrastructure.Respositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Learns.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMediatRServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(DomainContextTransactionBehavior<,>));
            return services.AddMediatR(typeof(Order).Assembly, typeof(Program).Assembly);
        }
        public static IServiceCollection AddDomainContext(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction)
        {
            return services.AddDbContext<DomainContext>(optionsAction);
        }

        public static IServiceCollection AddInMemoryDomainContext(this IServiceCollection services)
        {
            return services.AddDomainContext(builder => builder.UseInMemoryDatabase("domanContextDatabase"));
        }

        public static IServiceCollection AddMySqlDomainContext(this IServiceCollection services, string connectionString)
        {
            return services.AddDomainContext(builder =>
            {
                builder.UseMySql(connectionString);
            });
        }


        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUserRespository, UserRepository>();
            return services;
        }



        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            // 注入订阅服务
            services.AddTransient<ISubscriberService, SubscriberService>();
            services.AddCap(options =>
            {
                options.UseEntityFramework<DomainContext>();

                // 使用 rabbitmq 作为消息队列的存储
                options.UseRabbitMQ(options =>
                {
                    configuration.GetSection("RabbitMQ").Bind(options);
                });
                //options.UseDashboard();
            });

            return services;
        }
    }
}
