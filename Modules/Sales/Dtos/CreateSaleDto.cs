using PuntoVenta.Database.Entidades;
using System.ComponentModel.DataAnnotations.Schema;

namespace PuntoVenta.Modules.Sales.Dtos
{
    public class CreateSaleDto
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal Taxex { get; set; }
        public DateTime CreateDate { get; set; }
        public int CustomerId { get; set; }
        public int CashRegisterId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
        public EStatusCompra StatusCompra { get; set; }
        public List<CrearteSaleDetailDto>? SaleDetails { get; set; }
    }
}
