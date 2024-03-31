using System.ComponentModel.DataAnnotations.Schema;

namespace PuntoVenta.Database.Entidades
{
    public class Discount
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountedPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal MinimumDiscountQuantity { get; set; }

        public virtual List<ProductDiscount>? Products { get; set; }
    }
}
