using Microsoft.EntityFrameworkCore;

namespace EFCoreLearn.MyContext
{
    public class MyDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }
    }
}
