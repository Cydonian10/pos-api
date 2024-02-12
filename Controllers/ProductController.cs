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
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        public async Task<ActionResult> Create([FromBody] ProductoCrearDto productoCrearDto)
        {
            var productEntity = mapper.Map<Product>(productoCrearDto);

            await context.Products.AddAsync(productEntity!);
            await context.SaveChangesAsync();

            var productDto = mapper.Map<ProductDto>(productEntity);

            return Ok(productDto);

        }

        //[Authorize(Policy = "ManejadorProductos")]
        //[Authorize(Policy = "NivelAccesoTotal")]
        [HttpGet()]
        public async Task<ActionResult<List<ProductDto>>> ListPaginada([FromQuery] PageDto pageDto)
        {
            var queryble = context.Products
                            .Include(e => e.Category)
                            .Include(e => e!.UnitMeasurement)
                            .AsQueryable();

            await HttpContext.InsertarParametrosPaginar(queryble, pageDto.quantityRecordsPerPage);
            var productsDb = await queryble.Paginar(pageDto).ToListAsync();
            List<ProductDto> productDtos = mapper.Map<List<ProductDto>>(productsDb)!;
            return productDtos!;
        }


        //Warn arreglar Dto para actulizar producto
        [HttpPut("{id:int}")]
        public async Task<ActionResult> update([FromRoute] int id, [FromBody] ProductoCrearDto productocreardto)
        {
            var productuDB = await context.Products.FirstOrDefaultAsync(e => e.Id == id);

            if (productuDB == null) { return NotFound(); }

            if (productuDB!.SalePrice != productocreardto.SalePrice)
            {
                var historyPriceDb = new HistoryPriceProduct
                {
                    OldPrice = productuDB!.SalePrice,
                    ProductId = productuDB.Id,
                    Date = DateTime.UtcNow
                };
                await context.HistoryPriceProducts.AddAsync(historyPriceDb);
                await context.SaveChangesAsync();
            }

            mapper.Map(productocreardto, productuDB);


            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("filtro")]
        public async Task<ActionResult<List<ProductDto>>> Filtro([FromQuery] ProductoFiltroDto filtroDto)
        {
            var productQueryble = context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(filtroDto.Name))
            {
                productQueryble = productQueryble.Where(x => x.Name!.Contains(filtroDto.Name));
            }

            if (filtroDto.Stock != null)
            {
                productQueryble = productQueryble.Where(x => x.Stock == filtroDto.Stock);
            }

            if (filtroDto.Price != null)
            {
                productQueryble = productQueryble.Where(x => x.SalePrice >= filtroDto.Price);
            }

            var productsDb = await productQueryble
                                .Include(x => x.Category)
                                .Include(x => x.UnitMeasurement)
                                .ToListAsync();

            List<ProductDto> productDtos = mapper.Map<List<ProductDto>>(productsDb)!;

            return productDtos;
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            return await Delete<Product>(id);
        }
    }
}
