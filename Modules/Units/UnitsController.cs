using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PuntoVenta.Database;
using PuntoVenta.Database.Mappers;
using PuntoVenta.Modules.Units.Dtos;

namespace PuntoVenta.Modules.Units
{
    [Route("api/units")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class UnitsController : ControllerBase
    {
        private readonly DataContext context;

        public UnitsController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Authorize(Roles = "admin,vendedor")]
        public async Task<ActionResult<List<UnitDto>>> List() 
        {
            var unitsDB = await context.UnitMeasurements.ToListAsync();
            var unitsDTOS = unitsDB.Select(x => x.ToDto()).ToList();
            return unitsDTOS;
        }

        [HttpGet("{id:int}", Name = "ObtenerUnit")]
        [Authorize(Roles = "admin,vendedor")]
        public async Task<ActionResult<UnitDto>> GetOne([FromRoute] int id)
        {
            var unitDB = await context.UnitMeasurements.FindAsync(id);
            if(unitDB == null) { return NotFound(new { msg = $"La unidad con id: {id} no fue encontrada" }); }
            var unitDto = unitDB.ToDto();
            return unitDto;
     
        }

        [HttpGet("{nombre}/find", Name = "ObtenerUnitByNombre")]
        [Authorize(Roles = "admin,vendedor")]
        public async Task<ActionResult<UnitDto>> GetOneName([FromRoute] string nombre)
        {
            var unitDB = await context.UnitMeasurements.FirstOrDefaultAsync(x => x.Name == nombre); 
            if (unitDB == null) { return NotFound(new { msg = $"La unidad con id: {nombre} no fue encontrada" }); }
            var unitDto = unitDB.ToDto();
            return unitDto;

        }

        [HttpGet("filter")]
        [Authorize(Roles = "admin,vendedor")]
        public async Task<ActionResult<List<UnitDto>>> Filter([FromQuery] FilterUnitDto dto)
        {
            var unitQueryble = context.UnitMeasurements.AsQueryable();

            if (!string.IsNullOrEmpty(dto.Name))
            {
                unitQueryble = unitQueryble.Where(x => x.Name!.Contains(dto.Name));
            }

            var unitsDB = await unitQueryble.ToListAsync();
            return unitsDB.Select(x => x.ToDto()).ToList(); 
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Create([FromBody] CreateUnitDto dto)
        {
            var unit = dto.ToEntity();
            await context.AddAsync(unit);
            await context.SaveChangesAsync();
            var unitDTO = unit.ToDto();

            return new CreatedAtRouteResult("ObtenerUnit", new { id = unit.Id }, unitDTO);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<UnitDto>> Update([FromRoute] int id, [FromBody] CreateUnitDto dto)
        {
            var unitDB = await context.UnitMeasurements.FindAsync(id);
            if(unitDB == null) { return NotFound(new {msg = $"La unidad con id:{id} no fue encontrada" }); }
            unitDB.ToUpdateEntity(dto);
            await context.SaveChangesAsync();
           
            return unitDB.ToDto();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var unitDB = await context.UnitMeasurements.FindAsync(id);
            if (unitDB == null) { return NotFound(new { msg = $"La unidad con id:{id} no fue encontrada" }); }

            context.UnitMeasurements.Remove(unitDB);
            await context.SaveChangesAsync();
            return Ok( new { IdEliminado = id });
        }

    }
}
