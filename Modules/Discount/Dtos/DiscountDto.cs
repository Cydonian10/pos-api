namespace PuntoVenta.Modules.Discount.Dtos
{
    public class DiscountDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal DiscountedPrice { get; set; }
        public decimal MinimumDiscountQuantity { get; set; }
    }
}
