using AutoMapper;
using PuntoVenta.Database.Entidades;
using PuntoVenta.Dtos;

namespace PuntoVenta.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // * Products

            CreateMap<ProductoCrearDto, Product>()
                    .ForMember(dest => dest.Image, opt => opt.Ignore())
                    .ForMember(dest => dest.ProductName, opt => opt.MapFrom(MapProduNameDtoProductName));

            CreateMap<Product, ProductDto>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(MapProductNameFromName));

        }

        private string? MapProductNameFromName(Product product, ProductDto dto)
        {
            var resultado = product.ProductName?.Name;
            if (resultado == null) { return null; }
            return resultado;
        }

        private ProductName? MapProduNameDtoProductName(ProductoCrearDto productoCrearDto, Product product)
        {
            var resultado = new ProductName();
            if (productoCrearDto.Name == null) { return null; }
            resultado.Name = productoCrearDto.Name;
            return resultado;
        }
    }
}
