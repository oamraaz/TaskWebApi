using Microsoft.EntityFrameworkCore;

namespace MyFirstApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Task> tasks { get; set; }
    }
}
