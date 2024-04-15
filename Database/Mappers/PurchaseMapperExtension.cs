using PuntoVenta.Database.Entidades;
using PuntoVenta.Modules.Purchases.Dtos;

namespace PuntoVenta.Database.Mappers
{
    public static class PurchaseMapperExtension
    {
        public static Purchase ToEntity(this CreatePurchaseDto dto)
        {
            return new Purchase
            {
                Taxes = dto.Taxes,
                Date = dto.Date,
                SupplierId = dto.SupplierId,
                PurchaseDetails = dto.Details!.Select(x => new PurchaseDetail
                {
                    Quantity = x.Quantity,
                    ProductId = x.Product!.Id,
                    SubTotal = x.SubTotal,
                    PurchasePrice = x.PurchasePrice
                }).ToList(),
            };
        }

        public static PurchaseDto ToDto(this Purchase purchase)
        {
            return new PurchaseDto
            {
                Id = purchase.Id,
                TotalPrice = purchase.TotalPrice,
                Date = purchase.Date,
                Taxes = purchase.Taxes,
                VaucherNumber = purchase.VaucherNumber,
                
            };
        }
    }
}
