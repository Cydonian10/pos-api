namespace PuntoVenta.Modules.CashRegister.Dtos
{
    public class CashRegisterCrearDto
    {
        private decimal _initialCash;
        private decimal? _totalCash;
        public string? Name { get; set; }
        public decimal InitialCash
        {
            get { return _initialCash; }
            set
            {
                _initialCash = value;
                // Establecer TotalCash igual a InitialCash
                _totalCash = value;
            }
        }
        public DateTime Date { get; set; }
        public decimal? TotalCash
        {
            get { return _totalCash; }
        }
    }
}
