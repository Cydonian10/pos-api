using System.ComponentModel.DataAnnotations.Schema;

namespace PuntoVenta.Database.Entidades
{
    public class HistoryCashRegister : IId
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int CashRegisterId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalCash { get; set; }
        public string? EmployedId { get; set; }
        public DateTime Date { get; set; }
        public virtual User? Employed { get; set; }

    }
}
