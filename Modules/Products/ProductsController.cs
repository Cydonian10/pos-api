using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PuntoVenta.Database;
using PuntoVenta.Database.Mappers;
using PuntoVenta.Helpers.Dtos;
using PuntoVenta.Helpers.Extensions;
using PuntoVenta.Modules.Products.Dtos;

namespace PuntoVenta.Modules.Products
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DataContext context;

        public ProductsController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> List([FromQuery] PageDto pageDto)
        {
            var queryble = context.Products
                .Include(x => x.Category)
                .Include(x => x.UnitMeasurement).AsQueryable();
            await HttpContext.InsertarParametrosPaginar(queryble,pageDto.quantityRecordsPerPage);
            var productsDB = await queryble.Paginar(pageDto).ToListAsync();
            return productsDB.Select(x => x.ToDto()).ToList();
        }

        [HttpGet("{id:int}", Name = "ObtnerProduct")]
        public async Task<ActionResult<ProductDto>> GetOne([FromRoute] int id)
        {
            var product = await context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (product == null) { return NotFound($"El producto con id: {id} no existe"); }

            return product.ToDto();
        }

        [HttpGet("filter")]
        public async Task<ActionResult<List<ProductDto>>> Filter([FromQuery] FilterProductDto dto)
        {
            var productQueryble = context.Products.AsQueryable();

            if( !string.IsNullOrEmpty(dto.Name) )
            {
                productQueryble = productQueryble.Where(x => x.Name!.Contains(dto.Name));
            }
            if (dto.Stock != null)
            {
                productQueryble = productQueryble.Where(x => x.Stock == dto.Stock);
            }

            if (dto.Price != null)
            {
                productQueryble = productQueryble.Where(x => x.SalePrice >= dto.Price);
            }

            var productsDb = await productQueryble
                                .Include(x => x.Category)
                                .Include(x => x.UnitMeasurement)
                                .ToListAsync();

            return productsDb.Select(x => x.ToDto()).ToList();  
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateProductDto dto)
        {
            var product = dto.ToEntity();
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
            var productDB = context.Products
                .Include(x => x.Category)
                .Include(x => x.UnitMeasurement)
                .FirstOrDefault(x => x.Id == product.Id);
            var productDTO = productDB!.ToDto();
            return new CreatedAtRouteResult("ObtnerProduct", new { id = product.Id }, productDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] CreateProductDto dto)
        {
            var productDB = await context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (productDB == null) { return NotFound($"El producto con id: {id} no existe"); }

            productDB.ToEntityUpdate(dto);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var productDB = await context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (productDB == null) { return NotFound($"La producto con id:{id} no fue encontrada"); }

            context.Products.Remove(productDB);
            await context.SaveChangesAsync();

            return Ok(new { id });
        }

    }
}
