namespace PuntoVenta.Modules.Auth.Dtos
{
    public class UserDto
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        public decimal Salary { get; set; }
        public string? Name { get; set; }
        public DateTime Birthday { get; set; }
        public int Age { get; set; }
        public string? Phone { get; set; }
        public string? DNI { get; set; }
        public bool Active { get; set; }

    }
}
