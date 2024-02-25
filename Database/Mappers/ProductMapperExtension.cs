using PuntoVenta.Database.Entidades;
using PuntoVenta.Modules.Categories.Dtos;
using PuntoVenta.Modules.Products.Dtos;
using PuntoVenta.Modules.Units.Dtos;

namespace PuntoVenta.Database.Mappers
{
    public static class ProductMapperExtension
    {
        public static Product ToEntity(this CreateProductDto dto)
        {
            return new Product
            {
                Stock = dto.Stock,
                SalePrice = dto.SalePrice,
                PurchasePrice = dto.PurchasePrice,
                PurchaseDesc = dto.PurchaseDesc,
                Type = dto.Type,
                Description = dto.Description,
                Size = dto.Size,
                CondicionDiscount = dto.CondicionDiscount,
                Name = dto.Name,
                CategoryId = dto.CategoryId,
                UnitMeasurementId = dto.UnitMeasurementId,
                
            };
        }

        public static ProductDto ToDto(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Stock = product.Stock,
                SalePrice = product.SalePrice,
                PurchasePrice = product.PurchasePrice,  
                PurchaseDesc = product.PurchaseDesc,
                Type = product.Type,
                Description = product.Description,
                Size = product.Size,
                CondicionDiscount= product.CondicionDiscount,
                Name = product.Name,
                Category = new CategoryDto
                            {
                                Id = product.Category!.Id,
                                Name = product.Category!.Name,
                                Description = product.Category.Description
                            },
                UnitMeasurement = new UnitDto
                            {
                                Id = product.UnitMeasurement!.Id,
                                Name = product.UnitMeasurement.Name,
                                Symbol = product.UnitMeasurement.Symbol,
                            },
            };
        }

        public static Product ToEntityUpdate(this Product product, CreateProductDto dto)
        {
            product.Stock = dto.Stock;
            product.SalePrice = dto.SalePrice;
            product.PurchasePrice = dto.PurchasePrice;
            product.PurchaseDesc = dto.PurchaseDesc;
            product.Type = dto.Type;
            product.Description = dto.Description;
            product.Size = dto.Size;
            product.CondicionDiscount = dto.CondicionDiscount;
            product.Name = dto.Name;
            product.CategoryId = dto.CategoryId;
            product.UnitMeasurementId = dto.UnitMeasurementId;

            return product;
        }

        public static PatchProductDto ToPachEntity(this Product product)
        {
            return new PatchProductDto
            {
                SalePrice = product.SalePrice,
            };
        }
    }
}
