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
                Date = DateTime.Now,
                CustomerId = dto.CustomerId,
                CashRegisterId = dto.CashRegisterId,
                EStatusCompra = dto.StatusCompra,
                TotalPrice = dto.TotalPrice,
                SaleDetails = dto.SaleDetails!.Select(
                    x => new SaleDetail
                    {
                        Quantity = x.Quantity,
                        ProductId = x.Product!.Id,
                        Descuento = x.Discount,
                        SubTotal = x.SubTotal
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
                Customer = sale.Customer?.ToDto(),
                Employed = sale.User?.ToSaleDto(),
                CashRegister = sale.CashRegister?.ToDto(),
                Products = sale.SaleDetails!.Select(x => x.ToSaleDetailDto()).ToList()
            };
        }

        public static SaleDetailDto ToSaleDetailDto(this SaleDetail saleDetail)
        {
            return new SaleDetailDto
            {
                Quantity = saleDetail.Quantity,
                SubTotal = saleDetail.SubTotal,
                Descuento = (decimal)saleDetail.Descuento!,
                Product = saleDetail.Product!.ToDto(),

            };
        }
    }
}
