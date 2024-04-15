using PuntoVenta.Database.Entidades;
using PuntoVenta.Modules.Products.Dtos;
using System.ComponentModel.DataAnnotations.Schema;

namespace PuntoVenta.Modules.Sales.Dtos
{
    public class CrearteSaleDetailDto
    {
        public ProductDto? Product { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Quantity { get; set; }


        [Column(TypeName = "decimal(18,2)")]
        public decimal SubTotal { get; set; }


        [Column(TypeName = "decimal(18,2)")]
        public decimal? Discount { get; set; }
    }
}
