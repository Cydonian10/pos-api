using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PuntoVenta.Database.Entidades
{
    public class User : IdentityUser
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }
        public string? Name { get; set; }
        public DateTime Birthday { get; set; }
        // public string Avatar { get; set; }
        public virtual List<Sale>? SaleCustomer { get; set; }
        public virtual List<Sale>? SaleEmployed { get; set; }
        public virtual List<HistoryCashRegister>? HistoryCashRegisters { get; set; }
    }
}
