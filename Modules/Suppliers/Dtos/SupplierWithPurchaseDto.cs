using PuntoVenta.Database.Entidades;
using PuntoVenta.Modules.Purchases.Dtos;

namespace PuntoVenta.Modules.Suppliers.Dtos
{
    public class SupplierWithPurchaseDto : SupplierDto
    {
        public List<PurchaseDto>? Purchases { get; set; }
    }
}
