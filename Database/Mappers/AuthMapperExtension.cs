using PuntoVenta.Database.Entidades;
using PuntoVenta.Modules.Auth.Dtos;
using PuntoVenta.Modules.Sales.Dtos;

namespace PuntoVenta.Database.Mappers
{
    public static class AuthMapperExtension
    {
        public static UserDto ToDto(this User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Birthday = user.Birthday,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                Salary = user.Salary,
                Name = user.Name,
            };
        }

        public static UserSaleDto ToSaleDto(this User user)
        {
            return new UserSaleDto
            {
                Id= user.Id,
                Email = user.Email,
                Name = user.Name,
            };
        }
    }
}
