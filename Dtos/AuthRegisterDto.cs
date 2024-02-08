namespace PuntoVenta.Dtos
{
    public class AuthRegisterDto : IAuthCredencial
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Username { get; set;}
        public DateTime Birthday { get; set; }
        public decimal Salary { get; set;}
        public string? Rol { get; set; }
    }
}
