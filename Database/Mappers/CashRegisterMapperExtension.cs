using PuntoVenta.Database.Entidades;
using PuntoVenta.Modules.CashRegister;
using PuntoVenta.Modules.CashRegister.Dtos;
using PuntoVenta.Modules.CashRegister.Queries;

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
                Open = cashRegister.Open,
                TotalCash = (decimal)cashRegister.TotalCash!,
                UserId = cashRegister.UserId,
            };
        }

        public static CashRegister ToEntity(this CreateCashRegisterDto dto)
        {
            return new CashRegister
            {
                InitialCash = dto.InitialCash,
                Name = dto.Name,
                Date = dto.Date,
                UserId = null
            };
        }

        public static CashRegisterHistoryDto ToCashHistoryDto(this CashHistoryWithEmpleado entityDB )
        {
            return new CashRegisterHistoryDto
            {
                Id = entityDB.HistoryCashRegister!.Id,
                Name = entityDB.HistoryCashRegister.Name,
                TotalCash = entityDB.HistoryCashRegister.TotalCash,
                CashRegisterId = entityDB.HistoryCashRegister.CashRegisterId,
                Date = entityDB.HistoryCashRegister.Date,   
                EmployedId = entityDB.HistoryCashRegister.EmployedId,
                NombreEmpleado = entityDB.EmployedName
            };
        }

        public static CashRegister ToUpdateOpenDto(this CashRegister entity, OpenCashRegisterDto dto)
        {
            entity.InitialCash = dto.InitialCash;
            entity.TotalCash = dto.InitialCash;
            entity.Open = true;
            entity.UserId = null;

            return entity;
        }

        public static PatchCashRegisterDto ToPatchEntity(this CashRegister entity)
        {
            return new PatchCashRegisterDto
            {
                UserId = entity.UserId,
            };
        }
    }
}
