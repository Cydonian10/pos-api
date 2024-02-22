using PuntoVenta.Database.Entidades;
using PuntoVenta.Modules.Products.Dtos;

namespace PuntoVenta.Modules.Categories.Dtos
{
    public class CategoryWithProductsDto : CategoryDto
    {
        public List<CategoryProductsDto>? Products { get; set; }
    }
}
