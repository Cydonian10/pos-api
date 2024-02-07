using Microsoft.EntityFrameworkCore;

namespace PuntoVenta.Helpers
{
    public static class HttpContextExtension
    {
        public static async Task InsertarParametrosPaginar<T>(this HttpContext httpContext, IQueryable<T> queryble, int quantityRecordsPerPage)
        {
            double quantity = await queryble.CountAsync();
            double quantityPages = Math.Ceiling(quantity / quantityRecordsPerPage);

            httpContext.Response.Headers.Append("cantidadPaginas", quantityPages.ToString());
        }
    }
}
