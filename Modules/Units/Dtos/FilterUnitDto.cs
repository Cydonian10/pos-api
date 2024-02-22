using System.ComponentModel.DataAnnotations;

namespace PuntoVenta.Modules.Units.Dtos
{
    public class FilterUnitDto
    {
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "La propiedad solo debe contener letras.")]
        public string? Name { get; set; }
    }
}
