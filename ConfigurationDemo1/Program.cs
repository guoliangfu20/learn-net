using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace ConfigurationDemo1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            // 购置配置的核心
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddInMemoryCollection(  // 注入内存的配置
                new Dictionary<string, string>{
                {"key1","value1" },
                {"key2","value2" },
                { "sect1:key4","value4"},
                { "sect1:key5","value5"},
                { "sect2:key6","value6"},
                { "sect2:section3:key7","value7"},
            });

            IConfigurationRoot configurationRoot = builder.Build();

            //IConfiguration config = configurationRoot;

            Console.WriteLine(configurationRoot["key1"]);
            Console.WriteLine(configurationRoot["key2"]);

            var section1 = configurationRoot.GetSection("sect1");  // 获取分层
            Console.WriteLine($"section1.key4: {section1["key4"]}");
            Console.WriteLine($"section1.key5: {section1["key5"]}");

            var section2 = configurationRoot.GetSection("sect2");
            Console.WriteLine($"section2.key6: {section2["key6"]}");

            var section3 = section2.GetSection("section3");
            Console.WriteLine($"section2#section3#key7: {section3["key7"]}");


            var s4 = configurationRoot.GetSection("sect2:section3");
            Console.WriteLine($"直接获取分层key7：{s4["key7"]}");

        }
    }
}
