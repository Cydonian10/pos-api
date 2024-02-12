using System.ComponentModel.DataAnnotations.Schema;

namespace PuntoVenta.Database.Entidades
{
    public class PurchaseDetail
    {
        public int Id { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SubTotal { get; set; }
        public int ProductId { get; set; }
        public int PurchaseId { get; set; }
        public virtual Product? Product { get; set; }
        public virtual Purchase? Purchase { get; set; }
    }
}
