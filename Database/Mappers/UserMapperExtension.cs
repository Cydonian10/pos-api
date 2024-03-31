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
            user.DateBirthday = dto.DateBirthday;
            user.Email = dto.Email;
            user.Name = dto.Name;
            user.UserName = dto.Email;
            user.Email = dto.Email;
            //user.PasswordHash = user.PasswordHash;

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

        //public static User ToEntityCustomer()
        //{

        //}

        public static UserDto ToDto(this User user)
        {
            int age = DateTime.Now.Year - user.DateBirthday.Year;
            if (DateTime.Now < user.DateBirthday.AddYears(age))
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
            int age = DateTime.Now.Year - user.DateBirthday.Year;
            if (DateTime.Now < user.DateBirthday.AddYears(age))
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
                Birthday = user.DateBirthday,
                Roles = roles.ToList(),
                Claims = claims.Select(c => new ClaimsDto { Typo = c.Type, Value = c.Value }).ToList(),
            };
        }

        public static CustomerDto ToCustomerDto(this User user)
        {
            return new CustomerDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }
    }
}
