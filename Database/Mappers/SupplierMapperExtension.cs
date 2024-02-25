using PuntoVenta.Database.Entidades;
using PuntoVenta.Dtos;
using PuntoVenta.Modules.Purchases.Dtos;
using PuntoVenta.Modules.Suppliers.Dtos;

namespace PuntoVenta.Database.Mappers
{
    public static class SupplierMapperExtension
    {
        public static Supplier ToEntity(this SuplierCreateDto dto)
        {
            return new Supplier
            {
                Adress = dto.Adress,
                Name = dto.Name,
                Phone = dto.Phone,
            };
        }

        public static SupplierDto ToDto(this Supplier entity)
        {
            return new SupplierDto
            {
                Adress = entity.Adress,
                Name = entity.Name,
                Phone = entity.Phone,
                Id = entity.Id
            };
        }

        public static SupplierWithPurchaseDto ToWithPurchaseDto(this Supplier entity)
        {
            return new SupplierWithPurchaseDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Phone = entity.Phone,
                Adress = entity.Adress,
                Purchases = entity.Purchases!.Select(x => new PurchaseDto
                {   
                    Id = x.Id,
                    Date = x.Date,
                    Taxes = x.Taxes,
                    TotalPrice = x.TotalPrice,
                    VaucherNumber = x.VaucherNumber
                }).ToList()
            };
        }

        public static Supplier ToEntityUpdate(this Supplier entity, SuplierCreateDto dto)
        {
            entity.Adress = dto.Adress;
            entity.Name = dto.Name;
            entity.Phone = dto.Phone;

            return entity;
        }
    }
}
