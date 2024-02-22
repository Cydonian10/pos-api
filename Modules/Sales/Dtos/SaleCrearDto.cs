using PuntoVenta.Database.Entidades;

namespace PuntoVenta.Modules.Sales.Dtos
{
    public class SaleCrearDto
    {
        public decimal Taxex { get; set; }
        public DateTime Date { get; set; }
        public string? CustomerId { get; set; }
        public int CashRegisterId { get; set; }
        public EStatusCompra statusCompra { get; set; }
        public List<SaleDetailCrearDto>? SaleDetails { get; set; }
    }
}
