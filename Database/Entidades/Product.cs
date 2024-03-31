using System.ComponentModel.DataAnnotations.Schema;

namespace PuntoVenta.Database.Entidades
{
    public class Product : IId
    {
        public int Id { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Stock { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SalePrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PurchasePrice { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public string? Size { get; set; }
        public string? Name { get; set; }
        public int BarCode { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal QuantitySale { get; set; }
        public int CategoryId { get; set; }
        public int UnitMeasurementId { get; set; }
        public int BrandId { get; set; }
        public virtual Brand? Brand { get; set; }
        public virtual Category? Category { get; set; }
        public virtual UnitMeasurement? UnitMeasurement { get; set; }
        public virtual List<SaleDetail>? SaleDetails { get; set; }
        public virtual List<PurchaseDetail>? PurchaseDetails { get; set; }
        public virtual List<ProductDiscount>? Discounts { get; set; }
    }
}
