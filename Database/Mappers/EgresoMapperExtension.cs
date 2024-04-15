using PuntoVenta.Database.Entidades;
using PuntoVenta.Modules.Egresos.Dtos;

namespace PuntoVenta.Database.Mappers
{
    public static class EgresoMapperExtension
    {
        public static Egresos ToEntity(this CreateEgresoDto createEgresoDto)
        {
            return new Egresos
            {
                CashRegisterId = createEgresoDto.CashRegisterId,
                CreateDate = createEgresoDto.CreateDate,
                Egreso = createEgresoDto.Egreso,
                Monto = createEgresoDto.Monto,  
                Name = createEgresoDto.Name  
            };
        }

        public static EgresoDto ToDto(this Egresos egresos)
        {
            return new EgresoDto
            {
                CashRegisterId = egresos.CashRegisterId,
                CreateDate = egresos.CreateDate,
                Egreso = egresos.Egreso,
                Id = egresos.Id,
                Monto = egresos.Monto,
                Name = egresos.Name
            };
        }
    }
}
