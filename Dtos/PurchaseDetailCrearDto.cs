namespace PuntoVenta.Dtos
{
    public class PurchaseDetailCrearDto
    {
        public decimal Quantity { get; set; }
        public int ProductId { get; set; }
        public decimal SubTotal { get; set; }
    }
}
