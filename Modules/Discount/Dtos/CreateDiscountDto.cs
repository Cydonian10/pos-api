namespace PuntoVenta.Modules.Discount.Dtos
{
    public class CreateDiscountDto
    {
        public string? Name { get; set; }
        public decimal DiscountedPrice { get; set; }
        public decimal MinimumDiscountQuantity { get; set; }

    }
}
