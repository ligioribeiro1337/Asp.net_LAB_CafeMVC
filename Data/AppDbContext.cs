using Microsoft.EntityFrameworkCore;
using CafeMVC.Models;
namespace CafeMVC.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
        public DbSet<CafeChain> CafeChains => Set<CafeChain>();
        public DbSet<Order> Orders => Set<Order>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.CafeChain)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CafeChainId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CafeChain>().HasData(
                new CafeChain { Id = 1, Name = "Вкусно и точка", CuisineType = "Фастфуд", Regions = "Москва, Санкт-Петербург", Menu = "Бургеры, картофель фри", FoundedYear = 2022 },
                new CafeChain { Id = 2, Name = "Теремок", CuisineType = "Русская", Regions = "Москва", Menu = "Блины, каши, супы", FoundedYear = 1998 },
                new CafeChain { Id = 3, Name = "Якитория", CuisineType = "Японская", Regions = "Москва, Екатеринбург", Menu = "Суши, роллы, рамен", FoundedYear = 2003 }
            );
            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, OrderNumber = "ORD-001", Dishes = "Бургер классический, Кола", OrderTime = new DateTime(2025, 1, 15, 12, 30, 0, DateTimeKind.Utc), Status = "Выполнен", CafeChainId = 1 },
                new Order { Id = 2, OrderNumber = "ORD-002", Dishes = "Блин с мясом, Чай", OrderTime = new DateTime(2025, 1, 16, 14, 0, 0, DateTimeKind.Utc), Status = "Готовится", CafeChainId = 2 }
            );
        }
    }
}
