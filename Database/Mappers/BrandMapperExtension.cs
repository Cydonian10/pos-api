using PuntoVenta.Modules.Brand.Dtos;
using PuntoVenta.Database.Entidades;

namespace PuntoVenta.Database.Mappers
{
    public static class BrandMapperExtension
    {

        public static Brand ToEntity(this CreateBrandDto brandDto)
        {
            return new Brand
            {
                Name = brandDto.Name,
                Description = brandDto.Description, 
            };
        }

        public static BrandDto ToDto(this Brand brand)
        {
            return new BrandDto
            {
                Id = brand.Id,
                Name = brand.Name,
                Image = brand.Image,
                Description = brand.Description,
            };
        }

        public static Brand ToEntityUpdate(this Brand brand,CreateBrandDto dto)
        {
            brand.Name = dto.Name;
            brand.Description = dto.Description;

            return brand;
        }
    }
}
