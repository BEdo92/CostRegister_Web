using Microsoft.EntityFrameworkCore;

namespace CostRegApp2.Data
{
    public class CostRegContext : DbContext
    { 
        public CostRegContext(DbContextOptions<CostRegContext> options) : base(options) { }

        public DbSet<Categories> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Costs> Costs { get; set; }
        public DbSet<Income> Income { get; set; }
        public DbSet<CostPlans> CostPlans { get; set; }
        public DbSet<Shops> Shops { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
        }
    }
}
