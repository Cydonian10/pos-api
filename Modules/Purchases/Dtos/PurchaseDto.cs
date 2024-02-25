using PuntoVenta.Database.Entidades;
using System.ComponentModel.DataAnnotations.Schema;

namespace PuntoVenta.Modules.Purchases.Dtos
{
    public class PurchaseDto
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Taxes { get; set; }

        public DateTime Date { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VaucherNumber { get; set; }
      
    }
}
