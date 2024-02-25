using System.ComponentModel.DataAnnotations;

namespace PuntoVenta.Modules.Suppliers.Dtos
{
    public class SupplierDto
    {
        public int Id { get; set; }

        [MaxLength(150)]
        public string? Name { get; set; }

        [MaxLength(150)]
        public string? Adress { get; set; }

        [MaxLength(9)]
        public string? Phone { get; set; }
    }
}
