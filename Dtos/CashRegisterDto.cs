namespace PuntoVenta.Dtos
{
    public class CashRegisterDto
    {
        public string? Name { get; set; }
        public decimal InitialCash { get; set; }
        public DateTime Date { get; set; }
        public bool Open { get; set; }
        public decimal? TotalCash { get; set; }

    }
}
