using System.ComponentModel.DataAnnotations;

namespace PuntoVenta.Database.Entidades
{
    public class Supplier : IId
    {
        public int Id { get; set; }

        [MaxLength(150)]
        public string? Name { get; set; }

        [MaxLength(150)]
        public string? Adress { get; set; }

        [MaxLength(9)]
        public string? Phone { get; set; }
        public virtual List<Purchase>? Purchases { get; set; }
    }
}
