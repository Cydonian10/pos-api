using Microsoft.AspNetCore.Identity;
using PuntoVenta.Modules.Auth.Dtos;
using System.Security.Claims;

namespace PuntoVenta.Modules.Users.Dtos
{
    

    public class UserWithRolesDto : UserDto
    {
        public List<string>? Roles { get; set; }
        public List<ClaimsDto>? Claims { get; set; }
    }
}
