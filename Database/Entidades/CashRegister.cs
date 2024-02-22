using System.ComponentModel.DataAnnotations.Schema;

namespace PuntoVenta.Database.Entidades
{
    public class CashRegister :IId
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? TotalCash { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? InitialCash { get; set; }
        public DateTime Date { get; set; }
        public bool Open { get; set; }
        public virtual List<Sale>? Sales { get; set; }
    }
}
