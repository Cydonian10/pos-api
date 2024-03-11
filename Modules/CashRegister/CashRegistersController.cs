using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PuntoVenta.Database;
using PuntoVenta.Database.Entidades;
using PuntoVenta.Database.Mappers;
using PuntoVenta.Modules.CashRegister.Dtos;
using PuntoVenta.Modules.CashRegister.Queries;
using System.Security.Claims;

namespace PuntoVenta.Modules.CashRegister
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/cash-registers")]
    [ApiController]
    public class CashRegistersController : ControllerBase
    {
        private readonly DataContext context;

        public CashRegistersController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<CashRegisterDto>>> List()
        {
            var cashRegistersDB = await context.CashRegisters.ToListAsync();
            return cashRegistersDB.Select(x => x.ToDto()).ToList();
        }

        [HttpGet("{id:int}", Name = "ObtnerCashRegister")]
        public async Task<ActionResult<CashRegisterDto>> GetOne([FromRoute] int id)
        {
            var cashRegister = await context.CashRegisters.FirstOrDefaultAsync(x => x.Id == id);

            if (cashRegister == null) { return NotFound($"No se encontro caja registradora con id {id}"); }

            return cashRegister.ToDto();
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateCashRegisterDto dto)
        {
            var cashRegister = dto.ToEntity();
            cashRegister.TotalCash = cashRegister.InitialCash;
            await context.CashRegisters.AddAsync(cashRegister);
            await context.SaveChangesAsync();

            return new CreatedAtRouteResult("ObtnerCashRegister", new { id = cashRegister.Id }, cashRegister.ToDto());
        }

        [HttpPut("close/{id:int}")]
        public async Task<ActionResult> Close([FromRoute] int id)
        {
            var cashRegisterDB = await context.CashRegisters.FirstOrDefaultAsync(x => x.Id == id);
            var employedId = HttpContext.User.Claims.Where(claim => claim.Type == ClaimTypes.NameIdentifier).FirstOrDefault()!.Value;

            if (cashRegisterDB == null) { return NotFound(); }
            if (cashRegisterDB.Open == false) { return Conflict("La caja registradora ya esta cerrada"); }

            var historyCashRegister = new HistoryCashRegister
            {
                CashRegisterId = cashRegisterDB.Id,
                Date = DateTime.Now,
                EmployedId = employedId,
                TotalCash = Math.Round((decimal)(cashRegisterDB.TotalCash - cashRegisterDB.InitialCash)!, 2),
                Name = cashRegisterDB.Name
            };

            cashRegisterDB.Open = false;

            await context.HistoryCashRegisters.AddAsync(historyCashRegister);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("open/{id:int}")]
        public async Task<ActionResult<CashRegisterDto>> Open([FromRoute] int id, [FromBody] OpenCashRegisterDto dto)
        {
            var cashRegisterDB = await context.CashRegisters.FirstOrDefaultAsync(x => x.Id == id);

            if (cashRegisterDB == null) { return NotFound(); };
            if(cashRegisterDB.Open == true ) { return Conflict("La caja registradora ya esta abierto primero cierrela"); }

            cashRegisterDB.InitialCash = dto.InitialCash;
            cashRegisterDB.TotalCash = dto.InitialCash;
            cashRegisterDB.Open = true;

            await context.SaveChangesAsync();

            return cashRegisterDB.ToDto();  
        }

        [HttpGet("history/{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<CashRegisterHistoryDto>>> HisotryCashRegister([FromRoute] int id) {
            var cashRegisterDB = await context.HistoryCashRegisters
                .Where(x => x.CashRegisterId == id)
                .Select(h => new CashHistoryWithEmpleado
                {
                    HistoryCashRegister = h,
                    EmployedName = h.Employed!.Name
                }).ToListAsync();

            return Ok(cashRegisterDB.Select( x => x.ToCashHistoryDto()).ToList());
        }

    }


}
