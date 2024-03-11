using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PuntoVenta.Modules.Products.Dtos
{
    public class CreateProductDto
    {
        [Required]
        public decimal Stock { get; set; }

        [Required]
        [Range(double.Epsilon, double.MaxValue)]
        public decimal SalePrice { get; set; }

        [Required]
        [Range(double.Epsilon, double.MaxValue)]
        public decimal PurchasePrice { get; set; }

        [Required]
        public decimal PurchaseDesc { get; set; }
        public string? Image { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }

        [Required]
        public string? Size { get; set; }
        public string? CondicionDiscount { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        [Range(double.Epsilon, double.MaxValue)]
        public int CategoryId { get; set; }

        [Required]
        [Range(double.Epsilon, double.MaxValue)]
        public int UnitMeasurementId { get; set; }

        public int BarCode { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalSales { get; set; }
    }
}

