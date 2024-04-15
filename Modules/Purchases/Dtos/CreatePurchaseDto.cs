using System.ComponentModel.DataAnnotations.Schema;

namespace PuntoVenta.Modules.Purchases.Dtos
{
    public class CreatePurchaseDto
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal Taxes { get; set; }
        public DateTime Date { get; set; }
        public int SupplierId { get; set; }
        public List<CreatePurchaseDetailDto>? Details { get; set; }

    }
}
