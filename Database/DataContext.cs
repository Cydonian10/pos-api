using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PuntoVenta.Database.Entidades;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PuntoVenta.Database
{
    public class DataContext : IdentityDbContext<User>
    {
        // 
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }
        public DbSet<UnitMeasurement> UnitMeasurements { get; set; }
        public DbSet<HistoryPriceProduct> HistoryPriceProducts { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseDetail> PurchaseDetails { get; set; }
        public DbSet<CashRegister> CashRegisters { get; set; }
        public DbSet<HistoryCashRegister> HistoryCashRegisters { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<ProductDiscount> ProductDiscounts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Egresos> Egresos { get; set; }
        public DbSet<AppInit> AppInit { get; set; }
        public DbSet<Empresa> Empresa { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(e =>
            {
                e.ToTable("Products");
                e.HasKey("Id");
                e.HasOne(e => e.UnitMeasurement).WithMany(e => e.Products).HasForeignKey(e => e.UnitMeasurementId).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(e => e.Category).WithMany(e => e.Products).HasForeignKey(e => e.CategoryId).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(e => e.Brand).WithMany(e => e.Products).HasForeignKey(e => e.BrandId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Discount>(e =>
            {
                e.ToTable("Discounts");
                e.HasKey("Id");
            });

            modelBuilder.Entity<ProductDiscount>(e =>
            {
                e.ToTable("ProductDiscount");
                e.HasKey(e => new { e.ProductId, e.DiscountId });
                e.HasOne(e => e.Product).WithMany(e => e.Discounts).HasForeignKey(e => e.ProductId).OnDelete(DeleteBehavior.Restrict); ;
                e.HasOne(e => e.Discount).WithMany(e => e.Products).HasForeignKey(e => e.DiscountId).OnDelete(DeleteBehavior.Restrict); ;
            });

            modelBuilder.Entity<Brand>(e =>
            {
                e.ToTable("Brands");
                e.HasKey("Id");
            });

            modelBuilder.Entity<Category>(e =>
            {
                e.ToTable("Categories");
                e.HasKey("Id");
            });

            //modelBuilder.HasSequence<int>("MiEntidad_Id_seq", schema: "dbo")
            //            .StartsAt(100)
            //            .IncrementsBy(1);

            modelBuilder.Entity<Sale>(e =>
            {
                e.ToTable("Sales");
                e.HasKey("Id");
                //e.Property(e => e.VaucherNumber).HasDefaultValueSql("next value for dbo.MiEntidad_Id_seq"); ;
                e.HasOne(e => e.Customer).WithMany(e => e.Shopping).HasForeignKey(e => e.CustomerId).OnDelete(DeleteBehavior.Restrict); ;
                e.HasOne(e => e.User).WithMany(e => e.Sales).HasForeignKey(e => e.userId).OnDelete(DeleteBehavior.Restrict); ;
                e.HasOne(e => e.CashRegister).WithMany(e => e.Sales).HasForeignKey(e => e.CashRegisterId).OnDelete(DeleteBehavior.Restrict); ;
            });

            modelBuilder.Entity<SaleDetail>(e =>
            {
                e.ToTable("SaleDetails");
                e.HasKey("Id");
                e.HasOne(e => e.Product).WithMany(e => e.SaleDetails).HasForeignKey(e => e.ProductId);
                e.HasOne(e => e.Sale).WithMany(e => e.SaleDetails).HasForeignKey(e => e.SaleId);
            });

            modelBuilder.Entity<HistoryPriceProduct>(e =>
            {
                e.ToTable("HistoryPriceProduct");
                e.HasKey("Id");
            });

            modelBuilder.Entity<Supplier>(e =>
            {
                e.ToTable("Suppliers");
                e.HasKey("Id");
            });

            //modelBuilder.HasSequence<int>("Purchase_Id_seq", schema: "dbo")
            //         .StartsAt(1)
            //         .IncrementsBy(1);

            modelBuilder.Entity<Purchase>(e =>
            {
                e.ToTable("Purchase");
                e.HasKey("Id");
                //e.Property(e => e.VaucherNumber).HasDefaultValueSql("next value for dbo.Purchase_Id_seq"); ;

                e.HasOne(e => e.Supplier).WithMany(e => e.Purchases).HasForeignKey(e => e.SupplierId).OnDelete(DeleteBehavior.Restrict); ;
            });

            modelBuilder.Entity<PurchaseDetail>(e =>
            {
                e.ToTable("PurchaseDetail");
                e.HasKey("Id");

                e.HasOne(e => e.Purchase).WithMany(e => e.PurchaseDetails).HasForeignKey(e => e.PurchaseId).OnDelete(DeleteBehavior.Restrict); ;
                e.HasOne(e => e.Product).WithMany(e => e.PurchaseDetails).HasForeignKey(e => e.ProductId).OnDelete(DeleteBehavior.Restrict); ;
            });

            modelBuilder.Entity<CashRegister>(e =>
            {
                e.ToTable("CashRegisters");
                e.HasKey("Id");
            });

            modelBuilder.Entity<HistoryCashRegister>(e =>
            {
                e.ToTable("HistoryCashRegisters");
                e.HasKey("Id");

                e.HasOne(e => e.Employed).WithMany(e => e.HistoryCashRegisters).HasForeignKey(e => e.EmployedId).OnDelete(DeleteBehavior.Restrict); ;
            });

            modelBuilder.Entity<Customer>(e =>
            {
                e.ToTable("Customers");
                e.HasKey("Id");

                e.HasMany(e => e.Shopping).WithOne(e => e.Customer).HasForeignKey(e => e.CustomerId).OnDelete(DeleteBehavior.Restrict); ;

            });

            modelBuilder.Entity<Egresos>(e =>
            {
                e.ToTable("Egresos");
                e.HasKey("Id");

                e.HasOne(e => e.CashRegister).WithMany(e => e.Egresos).HasForeignKey(e => e.CashRegisterId).OnDelete(DeleteBehavior.Restrict); ;
            });

            modelBuilder.Entity<AppInit>(e =>
            {
                e.ToTable("AppInit");
            });

            modelBuilder.Entity<Empresa>(e =>
            {
                e.ToTable("Empresa");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}