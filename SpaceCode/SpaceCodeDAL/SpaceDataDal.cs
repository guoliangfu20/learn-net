using SpaceCodeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SpaceCodeDAL
{
    public class SpaceDataDal
    {
        private readonly static Respository _respository = new Respository();

        /// <summary>
        /// 查找单个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SpaceData FindById(int id)
        {
            return _respository.FindByID<SpaceData>(id);
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="spaceData"></param>
        /// <returns></returns>
        public bool Add(SpaceData spaceData)
        {
            return _respository.Add<SpaceData>(spaceData);
        }
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool Add(List<SpaceData> list)
        {
            return _respository.Add(list);
        }
        /// <summary>
        /// 修改单体
        /// </summary>
        /// <param name="spaceData"></param>
        /// <returns></returns>
        public bool Update(SpaceData spaceData)
        {
            return _respository.Update(spaceData);
        }
        public bool Update(List<SpaceData> datas)
        {
            return _respository.Update(datas);
        }

        /// <summary>
        /// 根据id删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            return _respository.Delete<SpaceData>((d) => d.Id == id);
        }
        /// <summary>
        /// 多条件查询删除
        /// </summary>
        /// <param name="spaceData"></param>
        /// <returns></returns>
        public bool Delete(SpaceData spaceData)
        {
            return _respository.Delete(spaceData);
        }

        public bool DeleteAll()
        {
            return _respository.Delete<SpaceData>();
        }


        /// <summary>
        /// 查询是否存在
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public bool Existed(SpaceDataDTO query)
        {
            return _respository.GetAny<SpaceData>(d =>
                query.Id > 0 ? d.Id == query.Id : true &&
                query.Data > 0 ? d.Data == query.Data : true
            );
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
            return _respository.Pagination<SpaceData>(query.PageIndex, query.PageSize, out count, query.OrderBy,
                  (d) =>
                  query.Id > 0 ? d.Id == query.Id : true &&
                  query.Data > 0 ? d.Data == query.Data : true);
        }
        /// <summary>
        /// 获取多个id
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<SpaceData> GetByIds(List<int> ids)
        {
            return _respository.GetAllQuery<SpaceData>(d => ids.Contains(d.Id));
        }
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public List<SpaceData> GetAll()
        {
            return _respository.GetAllQuery<SpaceData>();
        }
    }
}
