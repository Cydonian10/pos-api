using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PuntoVenta.Database;
using PuntoVenta.Database.Mappers;
using PuntoVenta.Modules.Suppliers.Dtos;

namespace PuntoVenta.Modules.Suppliers
{
    [Route("api/suppliers")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly DataContext context;

        public SupplierController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]   
        public async Task<ActionResult<List<SupplierDto>>> List()
        {
            var suppliers = await context.Suppliers.ToListAsync();
            return suppliers.Select(x => x.ToDto()).ToList();
        }

        [HttpGet("{id:int}", Name = "ObtnerSupplier")]
        public async Task<ActionResult<SupplierWithPurchaseDto>> GetOne([FromRoute] int id)
        {
            var supplier = await context.Suppliers.Include(x => x.Purchases).FirstOrDefaultAsync(x => x.Id == id);
            if(supplier == null) { return NotFound(); }
            return supplier.ToWithPurchaseDto();
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] SuplierCreateDto dto)
        {
            var supplier = dto.ToEntity();
            await context.Suppliers.AddAsync(supplier);
            await context.SaveChangesAsync();
            return new CreatedAtRouteResult("ObtnerSupplier", new { id = supplier.Id }, supplier.ToDto());
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<SupplierDto>> Update([FromRoute] int id, [FromBody] SuplierCreateDto dto)
        {
            var supplier = await context.Suppliers.FirstOrDefaultAsync(x => x.Id == id);
            if (supplier == null) { return NotFound(); }

            supplier.ToEntityUpdate(dto);
            await context.SaveChangesAsync();
            return supplier.ToDto();
        }


        [HttpDelete("{id:int}")]
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
