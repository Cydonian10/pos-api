using System.ComponentModel.DataAnnotations;

namespace PuntoVenta.Modules.Categories.Dtos
{
    public class CreateCategoryDto
    {
        [Required]
        [MaxLength(20)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
