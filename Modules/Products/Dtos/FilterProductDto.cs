namespace PuntoVenta.Modules.Products.Dtos
{
    public class FilterProductDto
    {
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public decimal? Stock { get; set; }
    }
}
