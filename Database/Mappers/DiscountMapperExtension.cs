using PuntoVenta.Database.Entidades;
using PuntoVenta.Modules.Discount.Dtos;

namespace PuntoVenta.Database.Mappers
{
    public static class DiscountMapperExtension
    {
        public static DiscountDto ToDiscountDto(this ProductDiscount productDiscount)
        {
            return new DiscountDto
            {
                Id = productDiscount.Discount!.Id,
                Name = productDiscount.Discount!.Name,
                DiscountedPrice = productDiscount.Discount.DiscountedPrice,
                MinimumDiscountQuantity = productDiscount.Discount.MinimumDiscountQuantity
            };
        }
    }
}
