using PuntoVenta.Database.Entidades;
using PuntoVenta.Modules.Brand.Dtos;
using PuntoVenta.Modules.Categories.Dtos;
using PuntoVenta.Modules.Units.Dtos;
using System.ComponentModel.DataAnnotations.Schema;

namespace PuntoVenta.Modules.Products.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Stock { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SalePrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PurchasePrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public string? Image { get; set; }
        public string? Description { get; set; }
        public string? Size { get; set; }
        public string? Name { get; set; }
        public int BarCode { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal QuantitySale { get; set; }
        public virtual string? Category { get; set; }
        public virtual string? UnitMeasurement { get; set; }
        public virtual string? Brand { get; set; }

    }
}
