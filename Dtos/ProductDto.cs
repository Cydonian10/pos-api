using System.ComponentModel.DataAnnotations.Schema;

namespace PuntoVenta.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }

        public decimal Stock { get; set; }

        public decimal SalePrice { get; set; }

        public decimal PurchasePrice { get; set; }

        public decimal PurchaseDesc { get; set; }
        public string? Image { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }
        public string? Size { get; set; }
        public ProductNameDto? Detalle { get; set; }
    }
}
