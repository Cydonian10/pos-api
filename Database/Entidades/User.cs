using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PuntoVenta.Database.Entidades
{
    public class User : IdentityUser
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }

        [StringLength(100)]
        public string? Name { get; set; }
        public DateTime DateBirthday { get; set; }

        [StringLength(maximumLength: 7, MinimumLength = 7 ,ErrorMessage = "El campo {0} debe tener 8 caracteres")]
        public string? DNI { get; set; }
        
        [StringLength(maximumLength: 9, MinimumLength = 9 ,ErrorMessage = "El campo {0} debe tener 9 caracteres")]
        public string? Phone { get; set; }
        // public string Avatar { get; set; }
        public virtual List<Sale>? SaleCustomer { get; set; }
        public virtual List<Sale>? SaleEmployed { get; set; }
        public virtual List<HistoryCashRegister>? HistoryCashRegisters { get; set; }
    }
}
