using PuntoVenta.Dtos;

namespace PuntoVenta.Modules.Auth.Dtos
{
    public class AuthRegisterDto : IAuthCredencial
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public DateTime Birthday { get; set; }
        public decimal Salary { get; set; }
        public string? Rol { get; set; }
    }
}
