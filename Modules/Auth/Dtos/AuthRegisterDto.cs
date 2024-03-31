using System.ComponentModel.DataAnnotations;

namespace PuntoVenta.Modules.Auth.Dtos
{
    public class AuthRegisterDto : IAuthCredencial
    {
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
        public string? Name { get; set; }
        public DateTime DateBirthday { get; set; }
        public decimal Salary { get; set; }
        public string? Phone { get; set; }
        public string? DNI { get; set; }

    }
}
