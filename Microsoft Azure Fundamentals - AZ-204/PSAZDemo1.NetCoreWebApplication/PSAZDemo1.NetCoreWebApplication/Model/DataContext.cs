using Microsoft.EntityFrameworkCore;

namespace PSAZDemo1.NetCoreWebApplication.Model
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }
        public virtual DbSet<Employee> Employee { get; set; }

    }
}
