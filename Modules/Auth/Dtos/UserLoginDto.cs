namespace PuntoVenta.Modules.Auth.Dtos
{
    public class UserLoginDto : IAuthCredencial
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
