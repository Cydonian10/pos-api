using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PuntoVenta.Database;
using PuntoVenta.Database.Entidades;
using PuntoVenta.Database.Mappers;
using PuntoVenta.Helpers.Base;
using PuntoVenta.Modules.Egresos.Dtos;

namespace PuntoVenta.Modules.Egresos
{
    [Route("api/egresos")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Roles = "admin,vendedor")]
    [ApiController]
    public class EgresosController : CustomControllerBase
    {
        private readonly DataContext context;

        public EgresosController(DataContext context) : base(context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var egresos = await context.Egresos.ToListAsync();
            return Ok(egresos);
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<EgresoDto>>> Get([FromQuery] SearchEgreso searchEgreso)
        {
            var egresos = await context.Egresos
                    .Where(x =>
                        x.CashRegisterId == searchEgreso.CashRegisterId &&
                        x.CreateDate.Year == searchEgreso.CreateDate.Year &&
                        x.CreateDate.Month == searchEgreso.CreateDate.Month &&
                        x.CreateDate.Day == searchEgreso.CreateDate.Day)
                    .ToListAsync();

            return egresos.Select(x => x.ToDto()).ToList();
        }

        [HttpPost]
        public async Task<ActionResult<EgresoDto>> Post([FromBody] CreateEgresoDto createEgresoDto)
        {
            var cashRegisterDb = await context.CashRegisters.FirstOrDefaultAsync(x => x.Id == createEgresoDto.CashRegisterId);

            if (cashRegisterDb == null) { return NotFound(new { msg = "NO existe cash Register" }); }

            if (cashRegisterDb.Open == false) { return Conflict(new { msg = "La caja registradora no esta abireto" }); }
            if (cashRegisterDb.UserId == null) { return Conflict(new { msg = "No hay nadie haciendose cargo de la caja" }); }

            var egreso = createEgresoDto.ToEntity();

            if (createEgresoDto.Egreso == TypeEgreso.Ingreso)
            {
                cashRegisterDb.TotalCash += createEgresoDto.Monto;
            }
            else
            {
                cashRegisterDb.TotalCash -= createEgresoDto.Monto;
            }

            await context.Egresos.AddAsync(egreso);
            await context.SaveChangesAsync();

            return egreso.ToDto();
        }
    }
}
