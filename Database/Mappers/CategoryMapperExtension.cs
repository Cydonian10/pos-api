
using PuntoVenta.Database.Entidades;
using PuntoVenta.Modules.Categories.Dtos;
using PuntoVenta.Modules.Products.Dtos;

namespace PuntoVenta.Database.Mappers
{
    public static class CategoryMapperExtension
    {
    

        public static Category ToEntity(this CreateCategoryDto dto)
        {
            return new Category
            {
                Name = dto.Name,
                Description = dto.Description,
            };
        }

        public static CategoryDto ToDto(this Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
            };
        }

        public static CategoryWithProductsDto ToWithProductsDto(this Category category)
        {
            return new CategoryWithProductsDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                Products = category.Products!.Select(x => new CategoryProductsDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Stock = x.Stock,
                }).ToList()
            };
        }


        public static Category ToEntityUpdate(this Category category, CreateCategoryDto dto)
        {
            category.Name = dto.Name;
            category.Description = dto.Description;
            return category;
        }


    }
}
