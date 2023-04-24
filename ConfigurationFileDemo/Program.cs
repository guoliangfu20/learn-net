using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;

namespace ConfigurationFileDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            #region 读取文件
            //builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);

            //builder.AddIniFile("appsettings.ini");

            //IConfigurationRoot configurationRoot = builder.Build();

            //Console.WriteLine($"key1: {configurationRoot["key1"]}");
            //Console.WriteLine($"key2: {configurationRoot["key2"]}");
            //Console.WriteLine($"key3: {configurationRoot["key3"]}");
            //Console.ReadKey();


            //Console.WriteLine($"key1: {configurationRoot["key1"]}");
            //Console.WriteLine($"key2: {configurationRoot["key2"]}");
            //Console.WriteLine($"key3: {configurationRoot["key3"]}"); 
            #endregion

            #region 通过代码，监控文件变更

            //builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            //var configurationRoot = builder.Build();


            // 监控文件变更，但只能监控一次
            //IChangeToken changeToken = configurationRoot.GetReloadToken();
            //changeToken.RegisterChangeCallback(state =>
            //{
            //    Console.WriteLine($"key1: {configurationRoot["key1"]}");
            //    Console.WriteLine($"key2: {configurationRoot["key2"]}");
            //    Console.WriteLine($"key3: {configurationRoot["key3"]}");

            //    changeToken = configurationRoot.GetReloadToken();

            //}, configurationRoot);

            //Console.ReadKey();

            //// 一直监控文件变更
            //ChangeToken.OnChange(() => configurationRoot.GetReloadToken(), () =>
            //{
            //    Console.WriteLine($"key1: {configurationRoot["key1"]}");
            //    Console.WriteLine($"key2: {configurationRoot["key2"]}");
            //    Console.WriteLine($"key3: {configurationRoot["key3"]}");
            //});

            #endregion



            #region 将配置绑定到强类型对象上



            builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configurationRoot = builder.Build();

            var config = new ConfigModel()
            {
                key1 = "config key1",
                key4 = false,
                //key5 = 100
            };

            //configurationRoot.Bind(config);

            // 读取分层的配置.
            //configurationRoot.GetSection("Order").Bind(config);

            // 针对私有属性
            configurationRoot.GetSection("Order").Bind(config, option => option.BindNonPublicProperties = true);

            Console.WriteLine($"key1: {config.key1}");
            Console.WriteLine($"key4: {config.key4}");
            Console.WriteLine($"key5: {config.key5}");


            #endregion


        }

        class ConfigModel
        {
            public string key1 { get; set; }
            public bool key4 { get; set; }
            public int key5 { get; private set; }
        }
    }
}
