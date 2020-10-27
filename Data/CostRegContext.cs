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
        public DbSet<Settings> Settings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>()
                .HasMany(c => c.Costs)
                .WithOne(c => c.Category)
                .HasForeignKey(k => k.CategoryID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Categories>()
                .HasMany(c => c.CostPlans)
                .WithOne(c => c.Category)
                .HasForeignKey(k => k.CategoryID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Shops>()
                .HasMany(c => c.Costs)
                .WithOne(p => p.Shop)
                .HasForeignKey(k => k.ShopID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasMany(c => c.Costs)
                .WithOne(p => p.User)
                .HasForeignKey(k => k.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
             .HasMany(c => c.CostPlans)
             .WithOne(p => p.User)
             .HasForeignKey(k => k.UserId)
             .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
             .HasMany(c => c.Income)
             .WithOne(p => p.User)
             .HasForeignKey(k => k.UserId)
             .OnDelete(DeleteBehavior.Cascade);

            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Categories>().HasData(
              new Categories { CategoryId = 1, CategoryName = "Rezsi" },
              new Categories { CategoryId = 2, CategoryName = "Lakbér" },
              new Categories { CategoryId = 3, CategoryName = "Élelmiszer, háztartás" },
              new Categories { CategoryId = 4, CategoryName = "Ruházat" },
              new Categories { CategoryId = 5, CategoryName = "Sport" },
              new Categories { CategoryId = 6, CategoryName = "Hobbi" },
              new Categories { CategoryId = 7, CategoryName = "Egészség" },
              new Categories { CategoryId = 8, CategoryName = "Háztartási gépek, karbantartás" },
              new Categories { CategoryId = 9, CategoryName = "Extra" },
              new Categories { CategoryId = 10, CategoryName = "Egyéb" });

            modelBuilder.Entity<Shops>().HasData(
              new Shops { ShopId = 1, ShopName = "Aldi" },
              new Shops { ShopId = 2, ShopName = "Auchan" },
              new Shops { ShopId = 3, ShopName = "Bershka" },
              new Shops { ShopId = 4, ShopName = "C&A" },
              new Shops { ShopId = 5, ShopName = "NewYorker" },
              new Shops { ShopId = 6, ShopName = "Penny Market" },
              new Shops { ShopId = 7, ShopName = "Lidl" },
              new Shops { ShopId = 8, ShopName = "MediaMarkt" },
              new Shops { ShopId = 9, ShopName = "Rossmann" },
              new Shops { ShopId = 10, ShopName = "Reál" },
              new Shops { ShopId = 11, ShopName = "Tesco" },
              new Shops { ShopId = 12, ShopName = "Nem üzlet" });
        }
    }
}
