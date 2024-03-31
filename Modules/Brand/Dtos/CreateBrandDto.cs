using System.ComponentModel.DataAnnotations;

namespace PuntoVenta.Modules.Brand.Dtos
{
    public class CreateBrandDto
    {
        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "La logintud del campo {0} debe estar entre {2} y {1}")]
        public string? Name { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }

        public IFormFile? Image { get; set; }
    }
}

//[DataType(DataType.Date)]
