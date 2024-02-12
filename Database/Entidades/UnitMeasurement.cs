namespace PuntoVenta.Database.Entidades
{
    public class UnitMeasurement : IId
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Symbol { get; set; }
        public virtual List<Product>? Products { get; set; }
    }
}
