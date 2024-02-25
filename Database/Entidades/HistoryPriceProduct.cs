using System.ComponentModel.DataAnnotations.Schema;

namespace PuntoVenta.Database.Entidades
{
    public class HistoryPriceProduct
    {
        public int Id { get; set; }
        public int ProductId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal OldPrice { get; set; }
        public string? Name { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
