using System.ComponentModel.DataAnnotations.Schema;

namespace PuntoVenta.Modules.CashRegister.Dtos
{
    public class CashRegisterHistoryDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int CashRegisterId { get; set; }
        public decimal TotalCash { get; set; }
        public string? EmployedId { get; set; }
        public DateTime Date { get; set; }
        public string? Empleado { get; set; }
    }
}
