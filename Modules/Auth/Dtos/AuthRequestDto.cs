namespace PuntoVenta.Modules.Auth.Dtos
{
    public class AuthRequestDto
    {
        public string? Token { get; set; }
        public DateTime Expiración { get; set; }
    }
}
