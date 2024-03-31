using PuntoVenta.Database.Entidades;

using PuntoVenta.Modules.Auth.Dtos;

namespace PuntoVenta.Database.Mappers
{
    public static class AuthMapperExtension
    {
        public static User ToEntity(this AuthRegisterDto authRegisterDto)
        {
            return new User
            {
                UserName = authRegisterDto.Email,
                Email = authRegisterDto.Email,
                Salary = authRegisterDto.Salary,
                DateBirthday = authRegisterDto.DateBirthday,
                Name = authRegisterDto.Name,
                DNI = authRegisterDto.DNI,
                Phone = authRegisterDto.Phone,
            };
        }
        //public static UserDto ToDto(this User user)
        //{
        //    return new UserDto
        //    {
        //        Id = user.Id,
        //        Email = user.Email,
        //        Salary = user.Salary,
        //        Name = user.Name,
        //    };
        //}

        //public static UserSaleDto ToSaleDto(this User user)
        //{
        //    return new UserSaleDto
        //    {
        //        Id= user.Id,
        //        Email = user.Email,
        //        Name = user.Name,
        //    };
        //}
    }
}
