using PuntoVenta.Database.Entidades;
using PuntoVenta.Modules.Brand.Dtos;
using PuntoVenta.Modules.Categories.Dtos;
using PuntoVenta.Modules.Discount.Dtos;
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
                Description = dto.Description,
                Size = dto.Size,
                Name = dto.Name,
                BrandId = dto.BrandId,
                CategoryId = dto.CategoryId,
                UnitMeasurementId = dto.UnitMeasurementId,
                BarCode = dto.BarCode,
            };
        }

        public static Product ToEntityAddDiscounts(this Product product, List<CreateDiscountDto> dto)
        {
            product.Discounts!.AddRange(dto.Select(x => new ProductDiscount
            {
                ProductId = product.Id,
                Discount = new Discount
                {
                    DiscountedPrice = x.DiscountedPrice,
                    MinimumDiscountQuantity = x.MinimumDiscountQuantity,
                    Name = x.Name,
                }
            }));

            return product;
        }

        public static ProductDto ToDto(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Stock = product.Stock,
                SalePrice = product.SalePrice,
                PurchasePrice = product.PurchasePrice,
                Description = product.Description,
                Size = product.Size,
                Name = product.Name,
                BarCode = product.BarCode,
                QuantitySale = product.QuantitySale,
                Image = product.Image,
                Brand = product.Brand!.Name,
                Category = product.Category!.Name,
                UnitMeasurement = product.UnitMeasurement!.Name,

            };
        }



        public static Product ToEntityUpdate(this Product product, CreateProductDto dto)
        {
            product.Stock = dto.Stock;
            product.SalePrice = dto.SalePrice;
            product.PurchasePrice = dto.PurchasePrice;
            product.Description = dto.Description;
            product.Size = dto.Size;
            product.Name = dto.Name;
            product.CategoryId = dto.CategoryId;
            product.UnitMeasurementId = dto.UnitMeasurementId;
            product.BarCode = dto.BarCode;
            product.QuantitySale = product.QuantitySale;
            product.BrandId = dto.BrandId;

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
