using PuntoVenta.Database.Entidades;
using System.ComponentModel.DataAnnotations.Schema;

namespace PuntoVenta.Modules.Egresos.Dtos
{
    public class CreateEgresoDto
    {
        public string? Name { get; set; }
        public TypeEgreso Egreso { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Monto { get; set; }
        public int CashRegisterId { get; set; }
        public DateTime CreateDate { get; set; }
       
    }
}
