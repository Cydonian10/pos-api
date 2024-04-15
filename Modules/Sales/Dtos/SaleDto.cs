using PuntoVenta.Modules.CashRegister.Dtos;
using PuntoVenta.Modules.Customers.Dtos;

namespace PuntoVenta.Modules.Sales.Dtos
{
    public class SaleDto
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Taxes { get; set; }
        public DateTime Date { get; set; }
        public int VaucherNumber { get; set; }
        public CustomerDto? Customer { get; set; }
        public UserSaleDto? Employed { get; set; }
        public CashRegisterDto? CashRegister { get; set; }
        public List<SaleDetailDto>? Products { get; set; }
    }
}
