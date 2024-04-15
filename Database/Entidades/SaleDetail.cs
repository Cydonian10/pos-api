using System.ComponentModel.DataAnnotations.Schema;

namespace PuntoVenta.Database.Entidades
{
    public class SaleDetail
    {
        public int Id { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Quantity { get; set; }
        public int ProductId { get; set; }
        public int SaleId { get; set; }

        [Column(TypeName = "decimal(18,2)")]    
        public decimal SubTotal { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Descuento { get; set; }
        public virtual Product? Product { get; set; }
        public virtual Sale? Sale { get; set; }

    }
}

