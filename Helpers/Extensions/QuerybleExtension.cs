using PuntoVenta.Helpers.Dtos;

namespace PuntoVenta.Helpers.Extensions
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
