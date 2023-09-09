using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EFCoreLearn.MyContext
{
    public class MySqlDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public MySqlDbContext(DbContextOptions<MySqlDbContext> options)
            : base(options)
        {

        }
    }
}
