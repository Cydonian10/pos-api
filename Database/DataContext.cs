using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PuntoVenta.Database.Entidades;

namespace PuntoVenta.Database
{
    public class DataContext : IdentityDbContext<User>
    {
        // 
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductName> ProductNames {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(e =>
            {
                e.ToTable("Products");
                e.HasKey("Id");

                e.HasOne(e => e.ProductName).WithMany(e => e.Products).HasForeignKey(e => e.ProductNameId);
            });


            modelBuilder.Entity<ProductName>(e =>
            {
                e.ToTable("ProductNames");
                e.HasKey("Id");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}