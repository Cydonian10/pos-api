using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PuntoVenta.Database;
using PuntoVenta.Database.Entidades;
using PuntoVenta.Database.Mappers;
using PuntoVenta.Modules.CashRegister.Dtos;
using System.Security.Claims;

namespace PuntoVenta.Modules.CashRegister
{
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
        public async Task<ActionResult<List<CashRegisterDto>>> List()
        {
            var cashRegistersDB = await context.CashRegisters.ToListAsync();
            return cashRegistersDB.Select(x => x.ToDto()).ToList();
        }

        [HttpGet("{id:int}", Name = "ObtnerCashRegister")]
        public async Task<ActionResult<CashRegisterDto>> GetOne([FromRoute] int id)
        {
            var cashRegister = await context.CashRegisters.FirstOrDefaultAsync(x => x.Id == id);

            if(cashRegister == null) { return NotFound($"No se encontro caja registradora con id {id}"); }
            
            return cashRegister.ToDto();
        }

        [HttpPost]  
        public async Task<ActionResult> Create([FromBody] CashRegisterCrearDto dto)
        {
            var cashRegister = dto.ToEntity();
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

            cashRegisterDB.Open = false;

            var historyCashRegister = new HistoryCashRegister
            {
                CashRegisterId = cashRegisterDB.Id,
                Date = DateTime.Now,
                EmployedId = employedId,
                TotalCash = Math.Round((decimal)(cashRegisterDB.TotalCash - cashRegisterDB.InitialCash)!, 2),
                Name = cashRegisterDB.Name
            };

            await context.HistoryCashRegisters.AddAsync(historyCashRegister);
            await context.SaveChangesAsync();
            return NoContent();
        }

    }
}
