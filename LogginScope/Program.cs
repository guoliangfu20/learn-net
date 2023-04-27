using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace LogginScope
{
    /// <summary>
    /// 日志作用域
    /// 
    /// </summary>
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


            IServiceProvider service = serviceCollection.BuildServiceProvider();
            var logger = service.GetService<ILogger<Program>>();

            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                // BeginScope 记录一条上下文串联的日志
                using (logger.BeginScope("ScopeId:{scopeId}", Guid.NewGuid()))
                {
                    logger.LogInformation("这是Info");
                    logger.LogError("这是Error");
                    logger.LogTrace("这是Trace");
                }
                // 处理 Console 异步处理造成日志顺序
                System.Threading.Thread.Sleep(100);
                Console.WriteLine("============分割线=============");
            }
        }
    }
}
