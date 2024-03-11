using PuntoVenta.Database.Entidades;
using PuntoVenta.Modules.Users.Dtos;
using PuntoVenta.Modules.Auth.Dtos;
using PuntoVenta.Modules.Sales.Dtos;
using System.Security.Claims;

namespace PuntoVenta.Database.Mappers
{
    public static class UserMapperExtension
    {
        public static User ToEntityUpdate(this User user, AuthRegisterDto dto)
        {
            user.Salary = dto.Salary;
            user.Birthday = dto.Birthday;
            user.Email = dto.Email;
            user.Name = dto.Name;
            user.UserName = dto.Email;
            user.Email = dto.Email;
            user.PasswordHash = user.PasswordHash;

            return user;
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

        public static UserDto ToDto(this User user)
        {
            int age = DateTime.Now.Year - user.Birthday.Year;
            if (DateTime.Now < user.Birthday.AddYears(age))
            {
                age--;
            }

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Age = age,
                Salary = user.Salary,
            };
        }

        public static UserWithRolesDto ToWithRolesDto(this User user, IList<string> roles, IList<Claim> claims)
        {
            int age = DateTime.Now.Year - user.Birthday.Year;
            if (DateTime.Now < user.Birthday.AddYears(age))
            {
                age--;
            }

            return new UserWithRolesDto
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Age = age,
                Salary = user.Salary,
                Roles = roles.ToList(),
                Claims = claims.Select(c => new ClaimsDto { Typo = c.Type,Value = c.Value }).ToList(),
            };
        }
    }
}
