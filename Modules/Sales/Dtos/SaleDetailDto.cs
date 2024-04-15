using PuntoVenta.Modules.Products.Dtos;

namespace PuntoVenta.Modules.Sales.Dtos
{
    public class SaleDetailDto
    {
        public decimal Quantity { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Descuento { get; set; }
        public ProductDto? Product { get; set; }
    } 
}
