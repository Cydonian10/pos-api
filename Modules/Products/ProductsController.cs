using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PuntoVenta.Database;
using PuntoVenta.Database.Entidades;
using PuntoVenta.Database.Mappers;
using PuntoVenta.Helpers.Dtos;
using PuntoVenta.Helpers.Extensions;
using PuntoVenta.Modules.Products.Dtos;
using PuntoVenta.Services.StoreImage;


namespace PuntoVenta.Modules.Products
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DataContext context;
        private readonly IStoreImageService storeImage;

        public ProductsController(DataContext context, IStoreImageService storeImage)
        {
            this.context = context;
            this.storeImage = storeImage;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> List([FromQuery] PageDto pageDto)
        {
            var queryble = context.Products
                .Include(x => x.Category)
                .Include(x => x.UnitMeasurement).AsQueryable();
            await HttpContext.InsertarParametrosPaginar(queryble, pageDto.quantityRecordsPerPage);
            var total = await context.Products.CountAsync();
            var totalPages = Math.Ceiling((decimal)total / pageDto.QuantityRecordsPerPage);
            var productsDB = await queryble.Paginar(pageDto).ToListAsync();
            return Ok(new { totalPages, data = productsDB.Select(x => x.ToDto()).ToList() });
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

            if (dto == null)
            {
                return Ok();
            }

            if (!string.IsNullOrEmpty(dto.Name))
            {
                productQueryble = productQueryble.Where(x => x.Name!.Contains(dto.Name));
            }

            if (dto.BarCode != null)
            {
                productQueryble = productQueryble.Where(x => x.BarCode == dto.BarCode);
            }

            if (dto.Stock != null)
            {
                productQueryble = productQueryble.Where(x => x.Stock >= dto.Stock);
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
        public async Task<ActionResult> Create([FromForm] CreateProductDto dto)
        {
            var product = dto.ToEntity();

            if (dto.Image != null)
            {
                using (var ms = new MemoryStream())
                {
                    await dto.Image.CopyToAsync(ms);
                    var contenido = ms.ToArray();
                    var extension = System.IO.Path.GetExtension(dto.Image.FileName).ToLower();
                    var contentType = dto.Image.ContentType;
                    product.Image = await storeImage.SaveImage(contenido, extension, "Productos", contentType);

                }
            }

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
        public async Task<ActionResult> Update([FromRoute] int id, [FromForm] CreateProductDto dto)
        {
            var productDB = await context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (productDB == null) { return NotFound($"El producto con id: {id} no existe"); }

            productDB.ToEntityUpdate(dto);

            if (dto.Image != null)
            {
                using(var ms = new MemoryStream())
                {
                    await dto.Image.CopyToAsync(ms);
                    var contenido = ms.ToArray();
                    var extension = System.IO.Path.GetExtension(dto.Image.FileName).ToLower();
                    var contentType = dto.Image.ContentType;
                    productDB.Image = await storeImage.SaveImage(contenido, extension, "Productos", contentType);

                }
            }

            await context.SaveChangesAsync();

            return Ok(productDB);
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Path([FromRoute] int id, JsonPatchDocument<PatchProductDto> jsonPatchDto)
        {
            if (jsonPatchDto == null) { return BadRequest(); }
            var productDB = await context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (productDB == null) { return NotFound(); }

            var historyProduct = new HistoryPriceProduct
            {
                ProductId = id,
                Date = DateTime.Now,
                OldPrice = productDB.SalePrice,
                Name = productDB.Name
            };

            await context.HistoryPriceProducts.AddAsync(historyProduct);

            await context.SaveChangesAsync();

            var productDTO = productDB.ToPachEntity();

            jsonPatchDto.ApplyTo(productDTO, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            productDB.SalePrice = productDTO.SalePrice;

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


        [HttpGet("history/{id:int}")]
        public async Task<ActionResult> GetHistoryProducts([FromRoute] int id)
        {
            var historyProductsDB = await context.HistoryPriceProducts
                .Where(p => p.ProductId == id)
                .OrderByDescending(x => x.Date)
                .ToListAsync();

            return Ok(historyProductsDB);
        }

    }
}
