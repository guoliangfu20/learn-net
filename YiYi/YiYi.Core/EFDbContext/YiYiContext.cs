using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiYi.Core.DBManager;

namespace YiYi.Core.EFDbContext
{
    public class YiYiContext : DbContext
    {
        /// <summary>
        /// 数据库连接名称 
        /// </summary>
        public string DataBaseName = null;


        public YiYiContext() : base()
        {

        }

        public YiYiContext(string connection)
        {
            this.DataBaseName = connection;
        }

        public YiYiContext(DbContextOptions<YiYiContext> options)
         : base(options)
        {

        }

        public override void Dispose()
        {
            base.Dispose();
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (Exception ex)
            {
                throw (ex.InnerException as Exception ?? (ex ?? new Exception("程序出错了，已记录日志.")));
            }
        }

        public override DbSet<TEntity> Set<TEntity>()
        {
            return base.Set<TEntity>();
        }

        /// <summary>
        /// 设置跟踪状态
        /// </summary>
        public bool QueryTracking
        {
            set
            {
                this.ChangeTracker.QueryTrackingBehavior =
                       value ? QueryTrackingBehavior.TrackAll
                       : QueryTrackingBehavior.NoTracking;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //string connectionString = DBServerProvider.GetConnectionString(null);


            base.OnConfiguring(optionsBuilder);
        }




    }
}
