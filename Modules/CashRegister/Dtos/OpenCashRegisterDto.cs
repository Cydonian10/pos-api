using System.ComponentModel.DataAnnotations;

namespace PuntoVenta.Modules.CashRegister.Dtos
{
    public class OpenCashRegisterDto
    {
        [RegularExpression(@"^\d{0,15}(\.\d{1,2})?$", ErrorMessage = "El campo debe tener hasta 18 dígitos y dos decimales.")]
        [Required]
        public decimal InitialCash { get; set; }

      
    }
}
