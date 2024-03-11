using System.ComponentModel.DataAnnotations;

namespace PuntoVenta.Modules.CashRegister.Dtos
{
    public class CreateCashRegisterDto
    {

        [Required]
        public string? Name { get; set; }

        [RegularExpression(@"^\d{0,15}(\.\d{1,2})?$", ErrorMessage = "El campo debe tener hasta 18 dígitos y dos decimales.")]
        public decimal InitialCash { get; set; }

        [Required]
        public DateTime Date { get; set; }

    }
}
