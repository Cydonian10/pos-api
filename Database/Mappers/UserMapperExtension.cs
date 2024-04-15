using PuntoVenta.Database.Entidades;

using PuntoVenta.Modules.Auth.Dtos;
using PuntoVenta.Modules.Sales.Dtos;
using System.Security.Claims;

namespace PuntoVenta.Database.Mappers
{
    public static class UserMapperExtension
    {
        public static User ToEntityUpdate(this User user, UserRegisterDto dto)
        {
            user.Salary = dto.Salary;
            user.DateBirthday = dto.DateBirthday;
            user.Email = dto.Email;
            user.Name = dto.Name;
            user.UserName = dto.Email;
            user.Active = dto.Active;
            //user.PasswordHash = user.PasswordHash;

            return user;
        }

        public static User ToEntity(this UserRegisterDto authRegisterDto)
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
                Active = authRegisterDto.Active,

            };
        }

        public static UserDto ToDto(this User user)
        {
            var currentDate = DateTime.Today;
            int age = currentDate.Year - user.DateBirthday.Year;

            if (user.DateBirthday > currentDate.AddYears(-age))
            {
                age--;
            }

            return new UserDto
            {
                Birthday = user.DateBirthday,
                Email = user.Email,
                Name = user.Name,
                Id = user.Id,
                Salary = user.Salary,
                Age = age,
                DNI = user.DNI,
                Phone = user.Phone,
                Active= user.Active,
            };
        }

        public static UserSaleDto ToSaleDto(this User user)
        {
            return new UserSaleDto
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
            };
        }

        public static ProfileDto ToProfileDto(this User user, IList<string> roles, List<ClaimsDto> claims)
        {
            var currentDate = DateTime.Today;
            int age = currentDate.Year - user.DateBirthday.Year;

            if (user.DateBirthday > currentDate.AddYears(-age))
            {
                age--;
            }

            return new ProfileDto
            {
                Birthday = user.DateBirthday,
                Email = user.Email,
                Name = user.Name,
                Id = user.Id,
                Salary = user.Salary,
                Age = age,
                DNI = user.DNI,
                Phone = user.Phone,
                Roles = roles,
                Claims = claims
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
