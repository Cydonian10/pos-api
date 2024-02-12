namespace PuntoVenta.Dtos
{
    public class SaleDetailCrearDto
    {
        public decimal Quantity { get; set; }
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? Discount { get; set; }
    }
}
