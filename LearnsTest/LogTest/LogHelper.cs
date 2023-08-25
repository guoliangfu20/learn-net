using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTest
{
    public class LogHelper
    {
        public LogHelper()
        {
            // 加载Log4Net配置
            XmlConfigurator.Configure();
        }



        private static readonly ILog log = LogManager.GetLogger(typeof(LogHelper));

        public static void ConfigureLog4Net()
        {
            // 加载Log4Net配置
            XmlConfigurator.Configure();
        }

        public static void Info(string message)
        {
            log.Info(message);
        }

        public static void Warn(string message)
        {
            log.Warn(message);
        }

        public static void Error(string message)
        {
            log.Error(message);
        }

        public static void Fatal(string message)
        {
            log.Fatal(message);
        }
    }
}
