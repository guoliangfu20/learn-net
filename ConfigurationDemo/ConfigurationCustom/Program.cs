using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;

namespace ConfigurationCustom
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();

            //builder.Add(new MyConfigurationSource());

            // 使用自定义的暴露方法
            builder.AddMyConfiguration();

            var configurationRoot = builder.Build();

            //Console.WriteLine($"lastTime: {configurationRoot["lastTime"]}");


            // 监听变化

            ChangeToken.OnChange(() => configurationRoot.GetReloadToken(), () =>
            {
                Console.WriteLine($"lastTime: {configurationRoot["lastTime"]}");
            });
            Console.WriteLine("开始了...");

            Console.ReadKey();
        }
    }
}
