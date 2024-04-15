using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PuntoVenta.Database;
using PuntoVenta.Database.Mappers;
using PuntoVenta.Modules.Suppliers.Dtos;

namespace PuntoVenta.Modules.Suppliers
{
    [Route("api/suppliers")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly DataContext context;

        public SupplierController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Authorize(Roles = "admin,vendedor")]
        public async Task<ActionResult<List<SupplierDto>>> List()
        {
            var suppliers = await context.Suppliers.ToListAsync();
            return suppliers.Select(x => x.ToDto()).ToList();
        }

        [HttpGet("{id:int}", Name = "ObtnerSupplier")]
        [Authorize(Roles = "admin,vendedor")]
        public async Task<ActionResult<SupplierWithPurchaseDto>> GetOne([FromRoute] int id)
        {
            var supplier = await context.Suppliers.Include(x => x.Purchases).FirstOrDefaultAsync(x => x.Id == id);
            if (supplier == null) { return NotFound(new { Msg = "NO se encontro proveedor" }); }
            return supplier.ToWithPurchaseDto();
        }

        [HttpGet("search")]
        [Authorize(Roles = "admin,vendedor")]
        public async Task<ActionResult<List<SupplierDto>>> Search([FromQuery] string name )
        {
            var suppliers = await context.Suppliers
                    .Where(x => x.Name!.Contains(name)).ToListAsync();

            return suppliers.Select(x => x.ToDto()).ToList();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Create([FromBody] CreateSuplierDto dto)
        {
            var supplier = dto.ToEntity();
            await context.Suppliers.AddAsync(supplier);
            await context.SaveChangesAsync();
            return new CreatedAtRouteResult("ObtnerSupplier", new { id = supplier.Id }, supplier.ToDto());
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<SupplierDto>> Update([FromRoute] int id, [FromBody] CreateSuplierDto dto)
        {
            var supplier = await context.Suppliers.FirstOrDefaultAsync(x => x.Id == id);
            if (supplier == null) { return NotFound(new { Msg = "NO se encontro proveedor" }); }

            supplier.ToEntityUpdate(dto);
            await context.SaveChangesAsync();
            return supplier.ToDto();
        }


        [HttpDelete("{id:int}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var supplierDB = await context.Suppliers.FirstOrDefaultAsync(x => x.Id == id);
            if (supplierDB == null) { return NotFound($"El proveedor con id:{id} no fue encontrada"); }

            context.Suppliers.Remove(supplierDB);
            await context.SaveChangesAsync();

            return Ok(new { id });
        }
    }
}
