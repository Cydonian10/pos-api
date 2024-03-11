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

        [Column(TypeName = "decimal(18,2)")]
        public decimal PurchaseDesc { get; set; }
        public string? Image { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }
        public string? Size { get; set; }
        public string? CondicionDiscount { get; set; }
        public string? Name { get; set; }
        public int CategoryId { get; set; }
        public int UnitMeasurementId { get; set; }
        public int BarCode { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalSales { get; set; }
        public virtual Category? Category { get; set; }
        public virtual UnitMeasurement? UnitMeasurement { get; set; }
        public virtual List<SaleDetail>? SaleDetails { get; set; }
        public virtual List<PurchaseDetail>? PurchaseDetails { get; set; }
    }
}
