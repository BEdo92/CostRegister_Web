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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>()
                .HasMany(c => c.Costs)
                .WithOne(c => c.Category);

            modelBuilder.Entity<Categories>()
                .HasMany(c => c.CostPlans)
                .WithOne(c => c.Category);

            modelBuilder.Entity<Shops>()
                .HasMany(c => c.Costs)
                .WithOne(p => p.Shop);

            modelBuilder.Entity<User>()
             .HasMany(c => c.Costs)
             .WithOne(p => p.User);

            modelBuilder.Entity<User>()
             .HasMany(c => c.CostPlans)
             .WithOne(p => p.User);

            modelBuilder.Entity<User>()
             .HasMany(c => c.Income)
             .WithOne(p => p.User);

            //base.OnModelCreating(modelBuilder);
        }
    }
}
