namespace PuntoVenta.Dtos
{
    public class SaleCrearDto
    {
        public decimal Taxex { get; set; }
        public DateTime Date { get; set; }
        public string? CustomerId { get; set; }
        public List<SaleDetailCrearDto>? SaleDetails { get; set; }
    }
}
