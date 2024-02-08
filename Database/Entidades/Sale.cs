using System.ComponentModel.DataAnnotations.Schema;

namespace PuntoVenta.Database.Entidades
{
    public class Sale : IId
    {
        public int Id { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Taxes { get; set; }
        public DateTime Date { get; set; }
        public int VaucherNumber { get; set; }
        public int CustomerId { get; set; }
        public int EmployedId { get; set; }
        public User? Customer { get; set; }
        public User? Employed { get; set; }
        public virtual List<SaleDetail>? SaleDetails { get; set; }
    }
}
