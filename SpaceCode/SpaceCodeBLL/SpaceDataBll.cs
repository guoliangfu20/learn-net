using SpaceCodeCommons;
using SpaceCodeDAL;
using SpaceCodeModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace SpaceCodeBLL
{
    public class SpaceDataBll
    {
        private readonly static SpaceDataDal _spaceDataDal = new SpaceDataDal();

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="spaceData"></param>
        /// <returns></returns>
        public bool Add(SpaceData spaceData)
        {
            if (spaceData == null) return false;
            return _spaceDataDal.Add(spaceData);
        }
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool Add(List<SpaceData> list)
        {
            if (list == null || list.Count <= 0) return false;
            return _spaceDataDal.Add(list);
        }
        /// <summary>
        /// 修改单体
        /// </summary>
        /// <param name="spaceData"></param>
        /// <returns></returns>
        public bool Update(SpaceData spaceData)
        {
            if (spaceData == null || spaceData.Id <= 0) return false;
            return _spaceDataDal.Update(spaceData);
        }
        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        public bool Update(List<SpaceData> datas)
        {
            if (datas == null || datas.Count <= 0) return false;
            return _spaceDataDal.Update(datas);
        }

        /// <summary>
        /// 根据id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SpaceData FindOne(int id)
        {
            if (id <= 0) return null;
            return _spaceDataDal.FindById(id);
        }
        /// <summary>
        /// 根据id删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            if (id <= 0) return false;
            return _spaceDataDal.Delete(id);
        }
        /// <summary>
        /// 多条件查询删除
        /// </summary>
        /// <param name="spaceData"></param>
        /// <returns></returns>
        public bool Delete(SpaceData spaceData)
        {
            if (spaceData == null || spaceData.Id <= 0) return false;
            return _spaceDataDal.Delete(spaceData);
        }

        public bool DeleteAll()
        {
            return _spaceDataDal.DeleteAll();
        }

        /// <summary>
        /// 查询是否存在
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public bool Existed(SpaceDataDTO query)
        {
            return _spaceDataDal.Existed(query);
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="query"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<SpaceData> Pagination(SpaceDataDTO query, out int count)
        {
            count = 0;
            if (query == null) return null;
            if (string.IsNullOrEmpty(query.OrderBy))
            {
                query.OrderBy = "LastModificationTime";
            }
            return _spaceDataDal.Pagination(query, out count);
        }

        /// <summary>
        /// 将mdb文件转化为sql数据.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public bool AddFromMdb(string file, out int total, bool change = false)
        {
            total = 0;
            if (string.IsNullOrEmpty(file))
            {
                return false;
            }
            var lstMdb = LoadFromMdb(file);
            if (lstMdb == null || lstMdb.Count <= 0) return false;

            DeleteAll();

            List<SpaceData> inserts = new List<SpaceData>();
            lstMdb.ForEach(m =>
            {
                inserts.Add(new SpaceData()
                {
                    Data = m.Data,
                });
            });

            bool isOK = true;
            if (inserts.Count > 0)
            {
                LogsHelper.Logger("批处理开始时间：" + DateTime.Now);

                total = change ? ButchCalcThread(inserts.Select(d => d.Data).ToList()) : 0;

                LogsHelper.Logger("批处理结束时间：" + DateTime.Now);

                if (Add(inserts))
                {
                    isOK = true;
                    LogsHelper.Logger($"插入数据库时间:{DateTime.Now}", "插入数据库");
                }
            }
            return isOK;
        }

        /// <summary>
        /// 多任务并行执行
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        private int ButchCalcThread(List<int> nums)
        {
            int total = 0;

            for (int i = 0; i < nums.Count; i++)
            {
                Task t1 = Task.Run(() => nums[i] += 10);
                Task t2 = Task.Run(() => nums[i] -= 10);
                Task t3 = Task.Run(() => nums[i] *= 10);
                Task.WaitAll(t1, t2, t3);
                total += nums[i];
            }
            return total;
        }


        /// <summary>
        /// 解析mdb文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private List<DataMdbDTO> LoadFromMdb(string file)
        {
            MdbHelper mdbHelper = new MdbHelper(file);
            mdbHelper.Open();

            var tableNames = mdbHelper.GetTableNames();

            List<DataMdbDTO> lstMdb = new List<DataMdbDTO>();

            foreach (string tableName in tableNames)
            {
                // 查询 sql
                var sql = "select * from " + tableName;

                // 执行sql
                var dataTable = mdbHelper.GetDataBySql(sql);
                if (dataTable != null && dataTable.Rows != null && dataTable.Rows.Count > 0)
                {
                    lstMdb.AddRange(dataTable.ToList<DataMdbDTO>());
                }
            }
            mdbHelper.Close();

            LogsHelper.Logger($"解析完成时间:{DateTime.Now}", "MDB文件解析完成");

            return lstMdb;
        }
    }

    public class DataMdbDTO
    {
        public int Index { get; set; }
        public int Data { get; set; }
    }
}
