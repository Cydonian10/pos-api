using System.ComponentModel.DataAnnotations.Schema;

namespace PuntoVenta.Database.Entidades
{
    public enum TypeEgreso
    {
        Ingreso = 1, Gasto = 2,
    }

    public class Egresos
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public TypeEgreso Egreso { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Monto { get; set; }
        public int CashRegisterId { get; set; }
        public DateTime CreateDate { get; set; }
        public CashRegister? CashRegister { get; set; }
    }
}
