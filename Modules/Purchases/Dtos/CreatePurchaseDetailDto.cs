using PuntoVenta.Modules.Products.Dtos;
using System.ComponentModel.DataAnnotations.Schema;

namespace PuntoVenta.Modules.Purchases.Dtos
{
    public class CreatePurchaseDetailDto
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal Quantity { get; set; }

        public ProductDto? Product { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PurchasePrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SubTotal { get; set; }
    }
}
