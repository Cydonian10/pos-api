using AutoMapper;
using PuntoVenta.Database.Entidades;
using PuntoVenta.Dtos;

namespace PuntoVenta.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            // * Usuarios

            CreateMap<AuthRegisterDto, User>();
            CreateMap<User, UserDto>();

            // * Products

            CreateMap<ProductoCrearDto, Product>()
                    .ForMember(dest => dest.Image, opt => opt.Ignore())
                    .ForMember(dest => dest.ProductName, opt => opt.MapFrom(MapProduNameDtoProductName));

            CreateMap<Product, ProductDto>()
                    .ForMember(dest => dest.Detalle, opt => opt.MapFrom(MapProductNameFromName));

            // * Categorias

            CreateMap<CategoryCrearDto, Category>();
            CreateMap<Category, CategoryDto>();

        }

        private ProductNameDto? MapProductNameFromName(Product product, ProductDto dto)
        {
            var resultado = new ProductNameDto();

            resultado.Name = product.ProductName?.Name;
            resultado.Category = product.ProductName!.Category!.Name;

            if (resultado == null) { return null; }
            return resultado;
        }

        private ProductName? MapProduNameDtoProductName(ProductoCrearDto productoCrearDto, Product product)
        {
            var resultado = new ProductName();
            if (productoCrearDto.Name == null) { return null; }
            resultado.Name = productoCrearDto.Name;
            resultado.CategoryId = productoCrearDto.CategoryId;
            return resultado;
        }
    }
}
