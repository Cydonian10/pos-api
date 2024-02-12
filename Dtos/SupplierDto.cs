using System.ComponentModel.DataAnnotations;

namespace PuntoVenta.Dtos
{
    public class SupplierDto
    {
        [MaxLength(150)]
        public string? Name { get; set; }

        [MaxLength(150)]
        public string? Adress { get; set; }

        [MaxLength(9)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo debe contener solo números.")]
        public string? Phone { get; set; }
    }
}
