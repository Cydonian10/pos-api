using PuntoVenta.Database.Entidades;
using PuntoVenta.Modules.Sales.Dtos;

namespace PuntoVenta.Database.Mappers
{
    public static class SaleMapperExtension
    {
        public static Sale ToEntity(this CreateSaleDto dto)
        {
            return new Sale
            {
                Taxes = dto.Taxex,
                Date = dto.Date,
                CustomerId = dto.CustomerId,
                CashRegisterId = dto.CashRegisterId,
                EStatusCompra = dto.statusCompra,
                SaleDetails = dto.SaleDetails!.Select(
                    x => new SaleDetail
                            {
                                Quantity = x.Quantity,
                                ProductId = x.ProductId,
                                UnitPrice = x.UnitPrice,
                                Descuento = x.Discount,
                            }).ToList()
            };
        }


        public static SaleDto ToDto(this Sale sale)
        {
            return new SaleDto
            {
                Id = sale.Id,
                TotalPrice = sale.TotalPrice,
                Taxes = sale.Taxes,
                Date = sale.Date,
                VaucherNumber = sale.VaucherNumber,
                Customer = sale.Customer?.ToSaleDto(),
                Employed = sale.Employed?.ToSaleDto(),
                CashRegister = sale.CashRegister?.ToDto(),
                Products = sale.SaleDetails!.Select(x =>  x.ToSaleDetailDto()).ToList()
            };
        }

        public static SaleDetailDto ToSaleDetailDto(this SaleDetail saleDetail)
        {
            return new SaleDetailDto
            {
                Quantity = saleDetail.Quantity,
                SubTotal = saleDetail.SubTotal,
                Descuento = (decimal)saleDetail.Descuento!,
                ProductStock = saleDetail.Product!.Stock,
                ProductSalePrice = saleDetail.Product.SalePrice,
                ProductSize = saleDetail.Product.Size,
                ProductName = saleDetail.Product.Name,
                ProductUnitSymbol = saleDetail.Product.UnitMeasurement!.Symbol

            };
        }
    }
}
