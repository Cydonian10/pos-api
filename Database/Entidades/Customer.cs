namespace PuntoVenta.Database.Entidades
{
    public class Customer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? DNI { get; set; }
        public string? Address { get; set; }
        public int Points { get; set; }
    }
}
