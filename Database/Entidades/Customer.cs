namespace PuntoVenta.Database.Entidades
{
    public class Customer : IId
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? DNI { get; set; }
        public string? Address { get; set; }
        public int Points { get; set; }
        public virtual List<Sale>? Shopping { get; set; }
    }
}
