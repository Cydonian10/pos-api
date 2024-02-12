namespace PuntoVenta.Dtos
{
    public class SaleDetailDto
    {
        public decimal Quantity { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Descuento { get; set; }
        

        // * Productos
        public decimal ProductStock { get; set; }
        public decimal ProductSalePrice { get; set; }
        public string? ProductSize { get; set; }
        public string? ProductName { get; set; }

        // * Unit Meserument
        public string? ProductUnitSymbol { get; set; }


    }
}
