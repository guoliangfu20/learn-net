using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceCodeCommons
{
    public static class LogsHelper
    {
        private static readonly string logName = DateTime.Now.ToString("yyyyMMdd11");

        /// <summary>
        /// 记录日志
        /// ~/Logs/[yyyyMMdd].log
        /// </summary>
        /// <param name="msg">日志内容</param>
        /// <param name="des">日志title</param>
        public static void Logger(string msg, string des = "")
        {
            // Task.Run(() =>
            //{
            //    AppendLog(des, msg);
            //});
            AppendLog(des, msg);
        }

        /// <summary>
        /// 同步执行
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="des"></param>
        public static void LoggerSync(string msg, string des = "")
        {
            AppendLog(des, msg);
        }

        /// <summary>
        /// 追加日志
        /// </summary>
        /// <param name="des"></param>
        /// <param name="msg"></param>
        private static void AppendLog(string des, string msg)
        {
            if (string.IsNullOrEmpty(msg)) return;
            var filePath = $"{System.Web.HttpRuntime.AppDomainAppPath}/Logs";
            if (!System.IO.Directory.Exists(filePath))
            {
                System.IO.Directory.CreateDirectory(filePath);
            }
            string fullPath = $"{System.Web.HttpRuntime.AppDomainAppPath}/Logs/{logName}.log";

            if (!System.IO.File.Exists(fullPath))
            {
                // 生成完，立马释放io
                System.IO.File.Create(fullPath).Close();
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(DateTime.Now);
            if (!string.IsNullOrEmpty(des))
            {
                sb.Append($" [{des}] ");
            }
            sb.Append('\n');
            sb.Append(msg);
            sb.Append('\n');

            using (StreamWriter sw = File.AppendText(fullPath))
            {
                sw.WriteLine(sb.ToString());
            }
        }

    }
}
