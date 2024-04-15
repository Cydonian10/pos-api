using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PuntoVenta.Database;
using PuntoVenta.Database.Entidades;

namespace PuntoVenta.Helpers.Base
{
    [ApiController]
    public class CustomControllerBase : ControllerBase
    {
        private readonly DataContext context;


        public CustomControllerBase(DataContext context)
        {
            this.context = context;
        }

     
        protected async Task<ActionResult> Delete<TEntidad>(int id) where TEntidad : class, IId, new()
        {
            var existe = await context.Set<TEntidad>().AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new TEntidad() { Id = id });
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
