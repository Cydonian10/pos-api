namespace PuntoVenta.Dtos
{
    public class PurchaseCrearDto
    {
        public decimal Taxes { get; set; }
        public DateTime Date { get; set; }
        public int SupplierId { get; set; }

        public List<PurchaseDetailCrearDto>? Products { get; set; }
    }
}
