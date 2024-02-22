using PuntoVenta.Database.Entidades;
using PuntoVenta.Modules.CashRegister.Dtos;

namespace PuntoVenta.Database.Mappers
{
    public static class CashRegisterMapperExtension
    {
        public static CashRegisterDto ToDto(this CashRegister cashRegister)
        {
            return new CashRegisterDto
            {
                Id = cashRegister.Id,
                Date = cashRegister.Date,
                InitialCash = (decimal)cashRegister.InitialCash!,
                Name = cashRegister.Name,
                Open = cashRegister.Open
            };
        }

        public static CashRegister ToEntity(this CashRegisterCrearDto dto)
        {
            return new CashRegister
            {
                InitialCash = dto.InitialCash,
                Name = dto.Name,
                Date = dto.Date,
                TotalCash = dto.TotalCash
            };
        }
    }
}
