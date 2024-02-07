using System.ComponentModel.DataAnnotations.Schema;

namespace PuntoVenta.Database.Entidades
{
    public class Product : IId
    {
        public int Id { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Stock { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SalePrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PurchasePrice { get; set;}

        [Column(TypeName = "decimal(18,2)")]
        public decimal PurchaseDesc { get; set; }
        public string? Image { get; set; }
        public string? Type { get; set;}
        public string? Description { get; set; }
        public string? Size { get; set;}
        public int? ProductNameId { get; set; }
        public virtual ProductName? ProductName { get; set;}
    }
}
