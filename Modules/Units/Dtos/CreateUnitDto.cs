using System.ComponentModel.DataAnnotations;

namespace PuntoVenta.Modules.Units.Dtos
{
    public class CreateUnitDto
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(30)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "La propiedad {0} solo debe contener letras.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(10)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "La propiedad {0} solo debe contener letras.")]
        public string? Symbol { get; set; }
    }
}
