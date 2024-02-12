namespace PuntoVenta.Dtos
{
    public class PurchaseDetailDto
    {
        public decimal Quantity { get; set; }
        public decimal SubTotal { get; set; }
        public string? ProductName { get; set; }
        public string? ProductUnit { get; set; }

    }
}
