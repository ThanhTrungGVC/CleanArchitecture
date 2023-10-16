using Microsoft.EntityFrameworkCore;
using Zata.Entities;
using Zata.ProductionManager.Domain.Entities;

namespace Zata.ProductionManager.MySql.EfCore
{
    public class ProductionDbContext : DbContext, IProductionDbContext
    {
        public ProductionDbContext()
        {
        }

        public ProductionDbContext(DbContextOptions<ProductionDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);
            });


            base.OnModelCreating(modelBuilder);
        }
    }
}