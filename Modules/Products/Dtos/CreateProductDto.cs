using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PuntoVenta.Modules.Products.Dtos
{
    public class CreateProductDto
    {
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Stock { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SalePrice { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PurchasePrice { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
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
        public int CategoryId { get; set; }

        [Required]
        public int UnitMeasurementId { get; set; }
    }
}
