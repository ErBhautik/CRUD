using Microsoft.EntityFrameworkCore;

namespace CRUD.Context
{
    public class CrudDbContext : DbContext
    {
        public CrudDbContext(DbContextOptions<CrudDbContext> options) : base(options)
        {
        }
        public DbSet<EvenTask> EvenTask { get; set; }
        public DbSet<OddTask> OddTask { get; set; }
        public DbSet<TaskHistory> TaskHistory { get; set; }
    }
}
