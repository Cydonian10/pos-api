namespace PuntoVenta.Modules.Auth.Dtos
{
    public class UserTokenDto
    {
        public string? Token { get; set; }
        public DateTime Expiración { get; set; }
        public UserDto? User { get; set; }
    }
}
