using EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SpaceCodeDAL
{
    public class Respository : IRespository, IDisposable
    {
        //此处进行调用EF的DBContent 的实体类或者通过工厂化模式来进行调用。
        private readonly static DbContext _dbContext = new spaceCodeDBEntities();

        /// <summary>
        /// 添加一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Entity"></param>
        /// <returns></returns>
        public bool Add<T>(T Entity) where T : class
        {
            using (TransactionScope Ts = new TransactionScope(TransactionScopeOption.Required))
            {
                _dbContext.Set<T>().Add(Entity);
                int Count = _dbContext.SaveChanges();
                Ts.Complete();
                return Count > 0;
            }
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Entity"></param>
        /// <returns></returns>
        public bool Add<T>(List<T> Entities) where T : class
        {
            using (TransactionScope Ts = new TransactionScope(TransactionScopeOption.Required))
            {
                _dbContext.Set<T>().AddRange(Entities);
                int Count = _dbContext.SaveChanges();
                Ts.Complete();
                return Count > 0;
            }
        }

        /// <summary>
        /// 删除单个实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Entity"></param>
        /// <returns></returns>
        public bool Delete<T>(T Entity) where T : class
        {
            using (TransactionScope Ts = new TransactionScope(TransactionScopeOption.Required))
            {
                _dbContext.Set<T>().Attach(Entity);
                _dbContext.Set<T>().Remove(Entity);
                int Count = _dbContext.SaveChanges();
                Ts.Complete();
                return Count > 0;
            }
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public bool Delete<T>(Expression<Func<T, bool>> whereLambda = null) where T : class
        {
            using (TransactionScope Ts = new TransactionScope(TransactionScopeOption.Required))
            {
                //var EntityModel = _dbContext.Set<T>().Where(whereLambda).FirstOrDefault();
                //if (EntityModel != null)
                //{
                //    _dbContext.Set<T>().Remove(EntityModel);
                //    int Count = _dbContext.SaveChanges();
                //    Ts.Complete();
                //    return Count > 0;
                //}

                int Count = 0;
                var Entities = whereLambda != null ?
                _dbContext.Set<T>().Where(whereLambda).ToList() ?? null
                : _dbContext.Set<T>().ToList() ?? null;
                if (Entities != null && Entities.Count() > 0)
                {
                    foreach (var item in Entities)
                    {
                        var EntityModel = _dbContext.Entry(item);
                        _dbContext.Set<T>().Attach(item);
                        EntityModel.State = EntityState.Deleted;
                    }
                    Count = _dbContext.SaveChanges();
                    Ts.Complete();
                }
                return Count > 0;
            }
        }

        /// <summary>
        ///  释放缓存
        /// </summary>
        public void Dispose()
        {
            _dbContext.Dispose();
        }

        /// <summary>
        /// 查询单个
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ID"></param>
        /// <returns></returns>
        public T FindByID<T>(object ID) where T : class
        {
            return _dbContext.Set<T>().Find(ID) ?? null;
        }

        /// <summary>
        /// 根据条件查询所有
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="WhereLambda"></param>
        /// <returns></returns>
        public List<T> GetAllQuery<T>(Expression<Func<T, bool>> WhereLambda = null) where T : class
        {
            return WhereLambda != null ?
                _dbContext.Set<T>().Where(WhereLambda).ToList() ?? null
                : _dbContext.Set<T>().ToList() ?? null;
        }

        /// <summary>
        /// 根据条件判断是否存在
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="WhereLambda"></param>
        /// <returns></returns>
        public bool GetAny<T>(Expression<Func<T, bool>> WhereLambda = null) where T : class
        {
            return WhereLambda != null ? _dbContext.Set<T>().Where(WhereLambda).Any() : _dbContext.Set<T>().Any();
        }

        /// <summary>
        /// 根据条件查询总数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="WhereLambda"></param>
        /// <returns></returns>
        public int GetCount<T>(Expression<Func<T, bool>> WhereLambda = null) where T : class
        {
            return WhereLambda != null ? _dbContext.Set<T>().Where(WhereLambda).Count() : _dbContext.Set<T>().Count();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="TotalCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="WhereLambda"></param>
        /// <param name="IsOrder"></param>
        /// <returns></returns>
        public List<T> Pagination<T, TKey>(int PageIndex, int PageSize, out int TotalCount, Expression<Func<T, TKey>> OrderBy, Expression<Func<T, bool>> WhereLambda = null, bool IsOrder = true) where T : class
        {
            //分页的时候一定要注意 Order一定在Skip 之前
            IQueryable<T> QueryList = IsOrder == true ? _dbContext.Set<T>().OrderBy(OrderBy) : _dbContext.Set<T>().OrderByDescending(OrderBy);

            if (WhereLambda != null)
            {
                QueryList = QueryList.Where(WhereLambda);
            }

            TotalCount = QueryList.Count();
            return QueryList.Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList() ?? null;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="TotalCount"></param>
        /// <param name="ordering"></param>
        /// <param name="WhereLambda"></param>
        /// <returns></returns>
        public List<T> Pagination<T>(int PageIndex, int PageSize, out int TotalCount, string ordering, Expression<Func<T, bool>> WhereLambda = null) where T : class
        {
            //分页的时候一定要注意 Order 一定在Skip 之前
            var QueryList = _dbContext.Set<T>().OrderBy(ordering);
            if (WhereLambda != null)
            {
                QueryList = QueryList.Where(WhereLambda);
            }

            TotalCount = QueryList.Count();
            return QueryList.Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList() ?? null;
        }

        /// <summary>
        /// 修改单个实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Entity"></param>
        /// <returns></returns>
        public bool Update<T>(T Entity) where T : class
        {
            using (TransactionScope Ts = new TransactionScope())
            {
                var EntityModel = _dbContext.Entry<T>(Entity);
                _dbContext.Set<T>().Attach(Entity);
                EntityModel.State = EntityState.Modified;
                int Count = _dbContext.SaveChanges();
                Ts.Complete();
                return Count > 0;
            }
        }

        /// <summary>
        /// 查询条件修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="WhereLambda"></param>
        /// <param name="UpdateLambda"></param>
        /// <returns></returns>
        public bool Update<T>(Expression<Func<T, bool>> WhereLambda, Expression<Func<T, T>> UpdateLambda) where T : class
        {
            _dbContext.Set<T>().Where(WhereLambda).Update<T>(UpdateLambda);
            return _dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Entity"></param>
        /// <returns></returns>
        public bool Update<T>(List<T> Entity) where T : class
        {
            int Count = 0;
            using (TransactionScope Ts = new TransactionScope(TransactionScopeOption.Required))
            {
                if (Entity != null)
                {
                    foreach (var item in Entity)
                    {
                        var EntityModel = _dbContext.Entry(item);
                        _dbContext.Set<T>().Attach(item);
                        EntityModel.State = EntityState.Modified;
                    }

                }
                Count = _dbContext.SaveChanges();
                Ts.Complete();
            }

            return Count > 0;
        }
    }
}
