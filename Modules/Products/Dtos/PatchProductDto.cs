using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PuntoVenta.Modules.Products.Dtos
{
    public class PatchProductDto
    {
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SalePrice { get; set; }
    }
}
