namespace PuntoVenta.Modules.Customers.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? DNI { get; set; }
        public string? Address { get; set; }
        public int Points { get; set; }
    }
}
