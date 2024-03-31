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
        public string? Description { get; set; }

        [Required]
        public string? Size { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int BrandId { get; set; }

        [Required]
        public int UnitMeasurementId { get; set; }

        [Required]
        [Range(double.Epsilon, double.MaxValue)]
        public int BarCode { get; set; }
         
        public IFormFile? Image { get; set; }
    }
}

