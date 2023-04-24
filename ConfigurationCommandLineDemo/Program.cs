using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System;

namespace ConfigurationCommandLineDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();

            // builder.AddCommandLine(args);


            #region 替换命令

            var mapper = new Dictionary<string, string> { { "-k1", "commandKey1" } };
            builder.AddCommandLine(args, mapper);

            #endregion



            IConfigurationRoot configurationRoot = builder.Build();

            Console.WriteLine($" commandKey1:{configurationRoot["commandKey1"]}");
            Console.WriteLine($" commandKey2:{configurationRoot["commandKey2"]}");
            Console.WriteLine($" commandKey3:{configurationRoot["commandKey3"]}");
            Console.WriteLine($" commandKey4:{configurationRoot["commandKey4"]}");

            Console.WriteLine($" k1:{configurationRoot["k1"]}");

            Console.ReadKey();
        }
    }
}
