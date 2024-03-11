using PuntoVenta.Database.Entidades;

namespace PuntoVenta.Modules.Sales.Dtos
{
    public class CreateSaleDto
    {
        public decimal Taxex { get; set; }
        public DateTime Date { get; set; }
        public string? CustomerId { get; set; }
        public int CashRegisterId { get; set; }
        public EStatusCompra statusCompra { get; set; }
        public List<CrearteSaleDetailDto>? SaleDetails { get; set; }
    }
}
