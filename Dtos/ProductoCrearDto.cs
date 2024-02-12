namespace PuntoVenta.Dtos
{
    public class ProductoCrearDto
    {
        public decimal Stock { get; set; }
        public decimal SalePrice { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal PurchaseDesc { get; set; }
        //public IFormFile? Image { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }
        public string? Size { get; set; }
        public int CategoryId { get; set; }
        public int UnitMeasurementId { get; set; }
        public string? CondicionDiscount { get; set; }
        public string? Name { get; set; }

    }
}   
