using System.ComponentModel.DataAnnotations;

namespace PuntoVenta.Modules.Units.Dtos
{
    public class CreateUnitDto
    {
        [Required]
        [MaxLength(30)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "La propiedad solo debe contener letras.")]
        public string? Name { get; set; }

        [Required]
        [MaxLength(10)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "La propiedad solo debe contener letras.")]
        public string? Symbol { get; set; }
    }
}
