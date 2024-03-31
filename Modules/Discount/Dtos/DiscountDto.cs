namespace PuntoVenta.Modules.Discount.Dtos
{
    public class DiscountDto
    {
        public string? Name { get; set; }
        public decimal DiscountedPrice { get; set; }
        public decimal MinimumDiscountQuantity { get; set; }
    }
}
