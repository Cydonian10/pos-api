using PuntoVenta.Dtos;

namespace PuntoVenta.Helpers
{
    public static class QuerybleExtension
    {
        public static IQueryable<T> Paginar<T>(this IQueryable<T> queryble, PageDto paginacionDto)
        {
            return queryble
              .Skip((paginacionDto.Page - 1) * paginacionDto.quantityRecordsPerPage)
              .Take(paginacionDto.quantityRecordsPerPage);
        }
    }
}
