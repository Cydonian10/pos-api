namespace PuntoVenta.Modules.Auth.Dtos
{
    public class UserDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public DateTime Birthday { get; set; }
        public string? Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public decimal Salary { get; set; }
    }
}
