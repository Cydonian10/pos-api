namespace PuntoVenta.Dtos
{
    public class AuthRequestDto
    {
        public string? Token { get; set; }
        public DateTime Expiración { get; set; }
    }
}
