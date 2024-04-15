namespace PuntoVenta.Helpers.Dtos
{
    public class PaginationDto
    {
        public int Page { get; set; } = 1;
        public int quantityRecordsPerPage = 10;
        public readonly int cantidadMaximaRegistrosPorPagina = 500;

        public int QuantityRecordsPerPage
        {
            get => quantityRecordsPerPage;
            set
            {
                quantityRecordsPerPage = (value > 500) ? cantidadMaximaRegistrosPorPagina : value;
            }
        }
    }
}
