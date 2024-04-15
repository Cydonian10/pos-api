using System.ComponentModel.DataAnnotations.Schema;

namespace PuntoVenta.Database.Entidades
{
    public enum EStatusCompra
    {
        Debe,
        Pagado,
        Cotizacion
    }

    public class Sale : IId
    {
        public int Id { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Taxes { get; set; }
        public DateTime Date { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VaucherNumber { get; set; }
        public int CustomerId { get; set; }
        public string? userId { get; set; }
        public int? CashRegisterId { get; set; }
        public EStatusCompra? EStatusCompra { get; set; }
        public Customer? Customer { get; set; }
        public User? User { get; set; }
        public virtual CashRegister? CashRegister { get; set; }
        public virtual List<SaleDetail>? SaleDetails { get; set; }
    }
}
