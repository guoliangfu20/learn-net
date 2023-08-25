using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceCodeCommons
{
    public class MdbHelper
    {
        private OleDbConnection _odcConnection;

        private string _fileName;

        private string _connectionString;

        //MAC在数据库文件中的名称
        string strMACSection = "";

        public MdbHelper(string mdbFileName)
        {
            this._fileName = mdbFileName;
            _connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + _fileName + ";";


        }
        /// <summary>
        /// 建立连接
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void Open()
        {
            try
            {
                // 建立连接 C#操作Access之读取mdb 
                _odcConnection = new OleDbConnection(this._connectionString);

                // 打开连接
                if (_odcConnection.State != ConnectionState.Open)
                {
                    _odcConnection.Open();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("尝试打开 " + this._fileName + " 失败, 请确认文件是否存在！");
            }
        }



        public DataTable GetDataBySql(string sql)
        {
            var dt = new DataTable();

            try
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter(sql, _odcConnection);
                adapter.Fill(dt);
            }
            catch (Exception)
            {

                return null;
            }
            return dt;
        }

        public void Close()
        {
            _odcConnection.Close();
        }

        /// <summary>
        /// 获取当前连接的mdb中的所有表名
        /// </summary>
        /// <returns></returns>
        public List<string> GetTableNames()
        {
            DataTable dt = _odcConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            List<string> tableNameList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var tableName = dt.Rows[i]["TABLE_NAME"].ToString();
                tableNameList.Add(tableName);
            }
            return tableNameList;
        }
    }
}
