using EFCoreSqlLearn.Model;
using Microsoft.EntityFrameworkCore;

namespace EFCoreSqlLearn.MyDbContext
{
    public class MySqlDbContext : DbContext
    {
        public DbSet<Order> orders { get; set; }
        public MySqlDbContext(DbContextOptions<MySqlDbContext> options)
            : base(options)
        {

        }
    }
}
