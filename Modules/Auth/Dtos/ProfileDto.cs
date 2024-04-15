using System.Security.Claims;

namespace PuntoVenta.Modules.Auth.Dtos
{
    public class ProfileDto : UserDto
    {
        public IList<string>? Roles { get; set; }
        public List<ClaimsDto>? Claims { get; set; }
    }
}
