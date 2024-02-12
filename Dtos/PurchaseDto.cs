using PuntoVenta.Database.Entidades;

namespace PuntoVenta.Dtos
{
    public class PurchaseDto
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Taxes { get; set; }
        public DateTime Date { get; set; }
        public int VaucherNumber { get; set; }
        public SupplierDto? Supplier { get; set; }
        public List<PurchaseDetailDto>? Products { get; set; }

    }
}
