using PuntoVenta.Database.Entidades;
using PuntoVenta.Modules.Empresa.Dtos;

namespace PuntoVenta.Database.Mappers
{
    public static class EmpresaMapperExtension
    {
        public static Empresa ToEntity(this CreateEmpresaDto empresadto)
        {
            return new Empresa
            {
                Address = empresadto.Address,
                Name = empresadto.Name,
                RUC = empresadto.RUC   
            };
        }

        public static EmpresaDto ToDto(this Empresa empresa)
        {
            return new EmpresaDto
            {
                Address = empresa.Address,  
                Name = empresa.Name,
                RUC = empresa.RUC,
                Id = empresa.Id,
                Image = empresa.Image   
            };
        }

        public static Empresa ToUpdateDto(this Empresa empresa  ,CreateEmpresaDto empresaDto)
        {
            empresa.Address = empresaDto.Address;
            empresa.Name = empresaDto.Name;
            empresa.RUC = empresaDto.RUC;

            return empresa;
        }
    }
}
