using System.ComponentModel.DataAnnotations.Schema;

namespace PuntoVenta.Modules.Purchases.Dtos
{
    public class CreatePurchaseDetailDto
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal Quantity { get; set; }
        public int ProductId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PurchasePrice { get; set; }
    }
}
