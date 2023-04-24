using Microsoft.Extensions.Configuration;
using System;

namespace ConfigurationEnvironmentVariablesDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();

            //builder.AddEnvironmentVariables();

            //IConfigurationRoot configurationRoot = builder.Build();

            //Console.WriteLine($"key1: {configurationRoot["key1"]}");
            //Console.WriteLine($"key2: {configurationRoot["GUO_KEY"]}");

            #region 分层键

            //IConfigurationSection section1 = configurationRoot.GetSection("SECTION1");
            //Console.WriteLine($"section1.key3: {section1["KEY3"]}");

            //IConfigurationSection section2 = configurationRoot.GetSection("SECTION1:SECTION2");
            //Console.WriteLine($"section2.key4: {section2["KEY4"]}");


            //var s2 = section1.GetSection("SECTION2");
            //Console.WriteLine($"第二种方式获取 section2.key4 ： {s2["KEY4"]}");

            #endregion


            #region 前缀过滤
            // 指定前缀的过滤
            builder.AddEnvironmentVariables("GUO_");
            var configurationBuilder2 = builder.Build();

            Console.WriteLine($" 前缀过滤, key: {configurationBuilder2["KEY"]} ");



            #endregion



        }
    }
}
