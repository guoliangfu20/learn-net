using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiYi.Core.DBManager
{
    public class DBServerProvider
    {
        private static Dictionary<string, string> ConnectionPool = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private static readonly string DefaultConnName = "defalut";

        public DBServerProvider()
        {
            
        }



        public static void SetConnection(string key, string val)
        {
            if (ConnectionPool.ContainsKey(key))
            {
                ConnectionPool[key] = val;
                return;
            }
            ConnectionPool.Add(key, val);
        }

        /// <summary>
        /// 设置默认数据库连接
        /// </summary>
        /// <param name="val"></param>
        public static void SetDefaultConnection(string val)
        {
            SetConnection(DefaultConnName, val);
        }

    }
}
