using Azure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PuntoVenta.Database;
using PuntoVenta.Database.Entidades;
using PuntoVenta.Database.Mappers;
using PuntoVenta.Helpers.Dtos;
using PuntoVenta.Helpers.Extensions;
using PuntoVenta.Modules.CashRegister.Dtos;
using PuntoVenta.Modules.CashRegister.Queries;
using PuntoVenta.Modules.Sales.Dtos;
using System.Security.Claims;

namespace PuntoVenta.Modules.CashRegister
{
    [Route("api/cash-registers")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class CashRegistersController : ControllerBase
    {
        private readonly DataContext context;

        public CashRegistersController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Authorize(Roles = "admin,vendedor")]
        public async Task<ActionResult<List<CashRegisterDto>>> List([FromQuery] PaginationDto paginationDto)
        {
            var cashRegistersDB = await context.CashRegisters.ToListAsync();
            return cashRegistersDB.Select(x => x.ToDto()).ToList();
        }

        [HttpGet("{id:int}", Name = "ObtnerCashRegister")]
        [Authorize(Roles = "admin,vendedor")]
        public async Task<ActionResult<CashRegisterDto>> GetOne([FromRoute] int id)
        {
            var cashRegister = await context.CashRegisters.FirstOrDefaultAsync(x => x.Id == id);

            if (cashRegister == null) { return NotFound($"No se encontro caja registradora con id {id}"); }

            return cashRegister.ToDto();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Create([FromBody] CreateCashRegisterDto dto)
        {
            var cashRegister = dto.ToEntity();
            await context.CashRegisters.AddAsync(cashRegister);
            await context.SaveChangesAsync();

            return new CreatedAtRouteResult("ObtnerCashRegister", new { id = cashRegister.Id }, cashRegister.ToDto());
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<CashRegisterDto>> Put([FromRoute] int id, [FromBody] UpdateCashrRegisterDto dto)
        {
            var cashRegister = await context.CashRegisters.FirstOrDefaultAsync(x => x.Id == id);
            if (cashRegister == null) { return NotFound(new { message = "Cash Register not found" }); }

            cashRegister.Name = dto.Name;

            await context.SaveChangesAsync();

            return cashRegister.ToDto();
        }

        [HttpPut("close/{id:int}")]
        [Authorize(Roles = "admin,vendedor")]
        public async Task<ActionResult<CashRegisterDto>> Close([FromRoute] int id)
        {
            var cashRegisterDB = await context.CashRegisters.FirstOrDefaultAsync(x => x.Id == id);
            var employedId = HttpContext.User.Claims.Where(claim => claim.Type == ClaimTypes.NameIdentifier).FirstOrDefault()!.Value;

            if (cashRegisterDB == null) { return NotFound(); }
            if (cashRegisterDB.Open == false) { return Conflict(new { msg = "La caja registradora ya esta cerrada" }); }

            var historyCashRegister = new HistoryCashRegister
            {
                CashRegisterId = cashRegisterDB.Id,
                Date = DateTime.Now,
                EmployedId = employedId,
                TotalCash = Math.Round((decimal)(cashRegisterDB.TotalCash - cashRegisterDB.InitialCash)!, 2),
                Name = cashRegisterDB.Name
            };

            cashRegisterDB.Open = false;
            cashRegisterDB.UserId = null;

            await context.HistoryCashRegisters.AddAsync(historyCashRegister);
            await context.SaveChangesAsync();
            return cashRegisterDB.ToDto();
        }

        [HttpPut("open/{id:int}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<CashRegisterDto>> Open([FromRoute] int id, [FromBody] OpenCashRegisterDto dto)
        {
            var cashRegisterDB = await context.CashRegisters.FirstOrDefaultAsync(x => x.Id == id);

            if (cashRegisterDB == null) { return NotFound(); };

            if (cashRegisterDB.Open == true) { return Conflict(new { msg = "La caja registradora ya esta abierto primero cierrela" }); }

            if (!string.IsNullOrEmpty(cashRegisterDB.UserId))
            {
                return Conflict($"El usuario con id {cashRegisterDB.Id} ya esta trabajando aqui");
            }

            cashRegisterDB.ToUpdateOpenDto(dto);

            await context.SaveChangesAsync();

            return cashRegisterDB.ToDto();
        }

        [HttpPatch("{id:int}/user/{userId}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> ActivarRegistarCashWithUser(int id, string userId, JsonPatchDocument<PatchCashRegisterDto> jsonPatchDto)
        {
            if (jsonPatchDto == null) { return BadRequest(); }

            var registerCashDb = await context.CashRegisters.FirstOrDefaultAsync(x => x.Id == id);


            if (registerCashDb == null) { return NotFound(new { msg = "Caja Registradora no encontrada" }); }

            if (registerCashDb.Open == false) { return Conflict(new { msg = "La caja esta cerrada" }); }


            if (!string.IsNullOrEmpty(registerCashDb.UserId))
            {
                return Conflict(new { msg = $"El usuario con id {registerCashDb.Id} ya esta trabajando aqui" });
            }

            var registerPatch = registerCashDb.ToPatchEntity();

            var cashRegisterExisteWithUser = await context.CashRegisters.FirstOrDefaultAsync(x => x.UserId == userId);

            if (cashRegisterExisteWithUser != null) { return Conflict(new { message = "Ya estas trabajando en una caja" }); }


            jsonPatchDto.ApplyTo(registerPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            registerCashDb.UserId = registerPatch.UserId;
            await context.SaveChangesAsync();

            return NoContent();

        }

        //Selecionar Caja Registradora
        [HttpPut("{id:int}/user")]
        [Authorize(Roles = "admin,vendedor")]
        public async Task<ActionResult<CashRegisterDto>> PutAddUser([FromRoute] int id)
        {
            var userId = HttpContext.User.Claims.Where(claim => claim.Type == ClaimTypes.NameIdentifier).FirstOrDefault()!.Value;
            var cashRegisterDb = await context.CashRegisters.FirstOrDefaultAsync(x => x.Id == id);
            if (cashRegisterDb == null) { return NotFound(new { msg = "Cash Register not found" }); }
            var cashRegisterByUser = await context.CashRegisters.FirstOrDefaultAsync(x => x.UserId == userId);
            if(cashRegisterByUser != null) { return Conflict(new { msg = "Ya estas trabajando en una caja" }); }
            if (cashRegisterDb.Open == false) { return Conflict(new { msg = "La caja esta cerrada" }); }
            

            if (!string.IsNullOrEmpty(cashRegisterDb.UserId))
            {
                return Conflict(new { msg = $"El usuario con id {cashRegisterDb.Id} ya esta trabajando aqui" });
            }

            cashRegisterDb.UserId = userId;

            await context.SaveChangesAsync();

            return cashRegisterDb.ToDto();
        }


        [HttpGet("history/{id:int}")]
        [Authorize(Roles = "admin,vendedor")]
        [AllowAnonymous]
        public async Task<ActionResult<List<CashRegisterHistoryDto>>> HisotryCashRegister([FromRoute] int id)
        {
            var cashRegisterHistoyWithEmpleadoDB = await context.HistoryCashRegisters
                .Where(x => x.CashRegisterId == id)
                .Select(h => new CashHistoryWithEmpleado
                {
                    HistoryCashRegister = h,
                    Empleado = h.Employed!.Name
                }).ToListAsync();

            return Ok(cashRegisterHistoyWithEmpleadoDB.Select(x => x.ToCashHistoryDto()).ToList());
        }

    }


}
