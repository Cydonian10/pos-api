using PuntoVenta.Database.Entidades;

namespace PuntoVenta.Modules.CashRegister.Queries
{
    public class CashHistoryWithEmpleado
    {
        public HistoryCashRegister? HistoryCashRegister { get; set; }
        public string? EmployedName { get; set; }
    }
}
