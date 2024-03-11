namespace PuntoVenta.Modules.Auth.Dtos
{
    public class AuthLoginDto : IAuthCredencial
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
