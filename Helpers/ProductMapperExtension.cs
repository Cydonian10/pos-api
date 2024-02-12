using Microsoft.EntityFrameworkCore;
using PuntoVenta.Database;
using PuntoVenta.Database.Entidades;
using PuntoVenta.Dtos;
using System.Drawing;

namespace PuntoVenta.Helpers
{
    public static class ProductMapperExtension
    {

        //public static async Task<Product> ToEntity(this ProductoCrearDto dto, DataContext context)
        //{
        //    var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == dto.CategoryId)
        //            ?? throw new Exception("La categoría especificada no existe en la base de datos.");

        //    var productName = new ProductName()
        //    {
        //        Category = category
        //    };

        //    if (dto.Name != null)
        //    {
        //        productName.Name = dto.Name;
        //        context.ProductNames.Add(productName);
        //    }
        //    else if (dto.ProductNameId != null)
        //    {
        //        productName = await context.ProductNames.FirstOrDefaultAsync(x => x.Id == dto.ProductNameId)
        //                ?? throw new Exception("La categoría especificada no existe en la base de datos.");
        //    }

        //    return new Product()
        //    {
        //        Stock = dto.Stock,
        //        SalePrice = dto.SalePrice,
        //        PurchasePrice = dto.PurchasePrice,
        //        PurchaseDesc = dto.PurchaseDesc,
        //        Type = dto.Type,
        //        Description = dto.Description,
        //        Size = dto.Size,
        //        UnitMeasurementId = dto!.UnitMeasurementId,
        //        CondicionDiscount = dto?.CondicionDiscount,
        //        ProductName = productName
        //    };
        //}

        //public static void ToUpdateEntity(this ProductoCrearDto dto, Product product,ProductName? productName)
        //{
        //    product.Stock = dto.Stock;
        //    product.SalePrice = dto.SalePrice;
        //    product.PurchasePrice = dto.PurchasePrice;
        //    product.PurchaseDesc = dto.PurchaseDesc;
        //    product.Type = dto.Type;
        //    product.Description = dto.Description;
        //    product.Size = dto.Size;
        //    product.UnitMeasurementId = dto?.UnitMeasurementId ?? product.UnitMeasurementId;
        //    product.CondicionDiscount = dto?.CondicionDiscount;
        //    product.ProductName = product.ProductName;

        //    if (productName != null)
        //    {
        //        product.ProductName = productName;
        //    }

        //}


        //public static ProductDto ToDto(this Product product)
        //{
        //    return new ProductDto()
        //    {
        //        Id = product.Id,
        //        CondicionDiscount = product.CondicionDiscount,
        //        Description = product.Description,
        //        Stock = product.Stock,
        //        SalePrice = product.SalePrice,
        //        PurchasePrice = product.PurchasePrice,
        //        Image = product.Image,
        //        Type = product.Type,
        //        Size = product.Size,
        //        PurchaseDesc = product.PurchaseDesc,
        //        UnitMeasurement = product.UnitMeasurement?.Name,
        //        Name = product.ProductName?.Name,
        //        Category = product.ProductName?.Category?.Name,
        //    };
        //}
    }
}
