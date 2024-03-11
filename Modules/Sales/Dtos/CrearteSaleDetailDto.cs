namespace PuntoVenta.Modules.Sales.Dtos
{
    public class CrearteSaleDetailDto
    {
        public decimal Quantity { get; set; }
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? Discount { get; set; }
    }
}
