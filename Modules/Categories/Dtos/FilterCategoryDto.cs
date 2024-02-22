using System.ComponentModel.DataAnnotations;

namespace PuntoVenta.Modules.Categories.Dtos
{
    public class FilterCategoryDto
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "La propiedad solo debe contener letras.")]
        public string? Name { get; set; }
    }
}
