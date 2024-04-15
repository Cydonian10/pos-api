using System.ComponentModel.DataAnnotations;

namespace PuntoVenta.Modules.Customers.Dtos
{
    public class CreateCustomerDto
    {
        [Required(ErrorMessage = "El campo {0} es requerido la la lan")]
        public string? Name { get; set; }

        [StringLength(maximumLength: 9, MinimumLength = 9, ErrorMessage = "El campo {0} debe tener 9 caracteres")]
        public string? Phone { get; set; }

        [StringLength(maximumLength: 8, MinimumLength = 8, ErrorMessage = "El campo {0} debe tener 8 caracteres")]
        public string? DNI { get; set; }
        public string? Address { get; set; }
    }
}
