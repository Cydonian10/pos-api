using PuntoVenta.Database.Entidades;

namespace PuntoVenta.Modules.Units.Dtos
{
    public class UnitWithProductsDto : UnitDto
    {
        public List<Product>? Products { get; set; }
    }
}
