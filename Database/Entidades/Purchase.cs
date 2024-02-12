using System.ComponentModel.DataAnnotations.Schema;

namespace PuntoVenta.Database.Entidades
{
    public class Purchase
    {
        public int Id { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Taxes { get; set; }

        public DateTime Date { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VaucherNumber { get; set; }

        public int SupplierId { get; set; }
        public Supplier? Supplier { get; set; }
        public virtual List<PurchaseDetail>? PurchaseDetails { get; set; }

    }
}
