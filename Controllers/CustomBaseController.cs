using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PuntoVenta.Database;
using PuntoVenta.Database.Entidades;
using System.Security.Cryptography;

namespace PuntoVenta.Controllers
{
    public class CustomBaseController : ControllerBase
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        public CustomBaseController(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        protected async Task<ActionResult<List<TDto>>> List<TEntidad, TDto>() where TEntidad : class
        {
            var entidadesDB = await context.Set<TEntidad>().AsNoTracking().ToListAsync();
            var dtos = mapper.Map<List<TDto>>(entidadesDB);
            return dtos!;
        }
     
        protected async Task<ActionResult<TDto>> GetOne<TEntidad, TDto>(int id) where TEntidad : class, IId
        {
            var entidadDB = await context.Set<TEntidad>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (entidadDB == null) { return NotFound(); }
            var dtos = mapper.Map<TDto>(entidadDB);
            return dtos!;
        }

        protected async Task<ActionResult> Create<TEntidad, TCreacionDto, TDto>(TCreacionDto creacionDto, string nombreRuta) where TEntidad : class, IId
        {
            var entidad = mapper.Map<TEntidad>(creacionDto);
            context.Set<TEntidad>().Add(entidad!);
            await context.SaveChangesAsync();

            var entidadDto = mapper.Map<TDto>(entidad);

            return new CreatedAtRouteResult(nombreRuta, new { id = entidad!.Id }, entidadDto);
        }

        public async Task<ActionResult> Update<TEntidad, TCreacionDto>(int id, TCreacionDto generoCreateDto) where TEntidad : class, IId
        {
            var entidadDB = await context.Set<TEntidad>().FirstOrDefaultAsync(x => x.Id == id);

            if (entidadDB == null) { return NotFound(); }

            mapper.Map(generoCreateDto, entidadDB);

            await context.SaveChangesAsync();
            return NoContent();
        }

        public async Task<ActionResult> Delete<TEntidad>(int id) where TEntidad : class, IId
        {
            var entidadDb = await context.Set<TEntidad>().FirstOrDefaultAsync(x => x.Id == id);

            if (entidadDb == null) { return NotFound(); }

            context.Set<TEntidad>().Remove(entidadDb);

            await context.SaveChangesAsync();

            return NoContent();

        }
    }
}
