using PuntoVenta.Database.Entidades;
using PuntoVenta.Modules.Units.Dtos;

namespace PuntoVenta.Database.Mappers
{
    public static class UnitsMapperExtension
    {
        public static UnitMeasurement ToEntity(this CreateUnitDto dto)
        {
            return new UnitMeasurement
            {
                Name = dto.Name,
                Symbol = dto.Symbol,
            };
        }

        public static UnitDto ToDto(this UnitMeasurement unit)
        {
            return new UnitDto
            {
                Id = unit.Id,
                Name = unit.Name,
                Symbol = unit.Symbol,
            };
        }

        public static UnitMeasurement ToUpdateEntity(this UnitMeasurement entity, CreateUnitDto dto)
        {
            entity.Name = dto.Name;
            entity.Symbol = dto.Symbol;
            return entity;
        }
    }
}
