using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PuntoVenta.Database;
using PuntoVenta.Database.Entidades;
using PuntoVenta.Dtos;
using PuntoVenta.Helpers;

namespace PuntoVenta.Controllers
{
    [Route("api/products")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductController : CustomBaseController
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public ProductController(DataContext context, IMapper mapper) : base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> Create([FromBody] ProductoCrearDto productoCrearDto)
        {
            var productEntity = mapper.Map<Product>(productoCrearDto);
            await context.Products.AddAsync(productEntity!);
            await context.SaveChangesAsync();
            var productoDto = mapper.Map<ProductDto>(productEntity);
            return productoDto!;
        }

        //[Authorize(Policy = "ManejadorProductos")]
        //[Authorize(Policy = "NivelAccesoTotal")]
        [HttpGet()]
        public async Task<ActionResult<List<ProductDto>>> ListPaginada([FromQuery] PageDto pageDto)
        {
            var queryble = context.Products.Include(e => e.ProductName).ThenInclude(e => e!.Category).AsQueryable();
            await HttpContext.InsertarParametrosPaginar(queryble, pageDto.quantityRecordsPerPage);
            var productsDb = await queryble.Paginar(pageDto).ToListAsync();
            var productDto = mapper.Map<List<ProductDto>>(productsDb);
            return productDto!;
        }


        // Warn arreglar Dto para actulizar producto
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update([FromRoute]int id, [FromBody] ProductoCrearDto productoCrearDto)
        {
            var productuDb = await context.Products.Include(e => e.ProductName).FirstOrDefaultAsync(e => e.Id == id);
            if(productuDb == null) { return NotFound(); }
            productuDb = mapper.Map(productoCrearDto, productuDb);
            productuDb!.ProductName!.Id = productoCrearDto.ProductNameId;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("filtro")]
        public async Task<ActionResult<List<ProductDto>>> Filtro([FromQuery] ProductoFiltroDto filtroDto)
        {
            var productQueryble = context.Products.AsQueryable();

            if(!string.IsNullOrEmpty(filtroDto.Name))
            {
                productQueryble = productQueryble.Include(x => x.ProductName).Where(x => x.ProductName!.Name!.Contains(filtroDto.Name));
            }

            if(filtroDto.Stock != null)
            {
                productQueryble = productQueryble.Where(x => x.Stock == filtroDto.Stock);
            }

            if (filtroDto.Price != null)
            {
                productQueryble = productQueryble.Where(x => x.SalePrice >= filtroDto.Price);
            }

            var productsDb = await productQueryble.Include(x => x.ProductName).ToListAsync();
            return mapper.Map<List<ProductDto>>(productsDb)!;
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            return await Delete<Product>(id);
        }
    }
}
