using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.IO;


namespace YiYi.Core.Configuration
{
    public class AppSetting
    {
        private static Connection _connection;

        public static IConfiguration Configuration { get; private set; }

        public static string DbConnectionString
        {
            get { return _connection.DbConnectionString; }
        }


        public static string RedisConnectionString
        {
            get { return _connection.RedisConnectionString; }
        }

        public static bool UseRedis
        {
            get { return _connection.UseRedis; }
        }

        public static bool UseSignalR
        {
            get { return _connection.UseSignalR; }
        }


        public static string GetSettingString(string key)
        {
            return Configuration[key];
        }

        // 多个节点,通过.GetSection("key")["key1"]获取
        public static IConfigurationSection GetSection(string key)
        {
            return Configuration.GetSection(key);
        }


        public static void Init(IServiceCollection services, IConfiguration configuration)
        {
            Configuration = configuration;

            services.Configure<Connection>(configuration.GetSection("Connection"));

        }
    }

    public class Connection
    {
        public string DBType { get; set; }
        public string DbConnectionString { get; set; }
        public string RedisConnectionString { get; set; }
        public bool UseRedis { get; set; }
        public bool UseSignalR { get; set; }
    }

}
