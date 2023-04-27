using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace LoggingDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {

            IConfigurationBuilder builder = new ConfigurationBuilder();

            builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfigurationRoot configRoot = builder.Build();

            // 构造一个容器
            IServiceCollection serviceCollection = new ServiceCollection();

            // //用工厂模式将配置对象注册到容器管理
            //serviceCollection.AddSingleton<IConfiguration>(configRoot);  // 这种模式下，容器是不会管理生命周期
            serviceCollection.AddSingleton<IConfiguration>(p => configRoot);  // 交给容器管理生命周期


            serviceCollection.AddLogging(builder =>
            {
                builder.AddConfiguration(configRoot.GetSection("Logging"));
                builder.AddConsole();
                builder.AddDebug();
            });


            #region 使用 ILoggerFactory

            // 把容器 builder 出来
            //IServiceProvider service = serviceCollection.BuildServiceProvider();

            //ILoggerFactory loggerFactory = service.GetService<ILoggerFactory>();

            //ILogger alogger = loggerFactory.CreateLogger("alogger");

            //alogger.LogDebug(2001, "hi debug");
            //alogger.LogInformation("hello info");
            //alogger.LogWarning("opps warning"); 

            #endregion


            #region 强类型管理

            serviceCollection.AddTransient<OrderService>();
            IServiceProvider service = serviceCollection.BuildServiceProvider();

            OrderService orderService = service.GetService<OrderService>();
            orderService.Show();



            #endregion


        }
    }
}
