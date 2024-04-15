using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PuntoVenta.Database;
using PuntoVenta.Database.Mappers;
using PuntoVenta.Helpers.Dtos;
using PuntoVenta.Helpers.Extensions;
using PuntoVenta.Modules.Brand.Dtos;
using PuntoVenta.Services.StoreImage;

namespace PuntoVenta.Modules.Brand
{
    [Route("api/brand")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly DataContext context;
        private readonly IStoreImageService storeImageService;

        public BrandController(DataContext context, IStoreImageService storeImageService)
        {
            this.context = context;
            this.storeImageService = storeImageService;
        }

        [HttpGet]
        [Authorize(Roles = "admin,vendedor")]
        public async Task<ActionResult<List<BrandDto>>> Get([FromQuery] PaginationDto paginationDto)
        {
            var queryble = context.Brands.AsQueryable();

            await HttpContext.InsertarParametrosPaginar(queryble, paginationDto.quantityRecordsPerPage);
            var brandsDB = await queryble.Paginar(paginationDto).ToListAsync();

            return brandsDB.Select( x => x.ToDto() ).ToList();

        }


        [HttpGet("{id:int}", Name = "ObtenerBrand")]
        [Authorize(Roles = "admin,vendedor")]
        public async Task<ActionResult<BrandDto>> Get([FromRoute] int id)
        {
            var brandDB = await context.Brands.FirstOrDefaultAsync(x => x.Id == id);

            if (brandDB == null) { return NotFound($"La marca con id: {id} no existe"); }

            return brandDB.ToDto();
        }

        [HttpGet("{nombre}/find")]
        [Authorize(Roles = "admin,vendedor")]
        public async Task<ActionResult<BrandDto>> Get([FromRoute] string nombre)
        {
            var brandDB = await context.Brands.FirstOrDefaultAsync(x => x.Name == nombre);

            if (brandDB == null) { return NotFound($"La marca con id: {nombre} no existe"); }

            return brandDB.ToDto();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Create([FromForm] CreateBrandDto brandDto)
        {
            var brand = brandDto.ToEntity();

            if (brandDto.Image != null)
            {
                using var ms = new MemoryStream();
                await brandDto.Image.CopyToAsync(ms);
                var contenido = ms.ToArray();
                var extension = System.IO.Path.GetExtension(brandDto.Image.FileName).ToLower();
                var contentType = brandDto.Image.ContentType;
                brand.Image = await storeImageService.SaveImage(contenido, extension, "Brands", contentType);
            }

            await context.Brands.AddAsync(brand);
            await context.SaveChangesAsync();

            return new CreatedAtRouteResult("ObtenerBrand", new { id = brand.Id }, brand.ToDto());
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<BrandDto>> Put([FromForm] CreateBrandDto brandDto, [FromRoute] int id)
        {
            var brandDB = await context.Brands.FirstOrDefaultAsync(x => x.Id == id);

            if (brandDB == null) { return NotFound(new {msg = "Brand no encontrado"}); }

            brandDB.ToEntityUpdate(brandDto);

            if (brandDto.Image != null)
            {
                using var ms = new MemoryStream();
                await brandDto.Image.CopyToAsync(ms);
                var contenido = ms.ToArray();
                var extension = System.IO.Path.GetExtension(brandDto.Image.FileName).ToLower();
                var contentType = brandDto.Image.ContentType;
                brandDB.Image = await storeImageService.UpdateImage(contenido, extension, "Brands", contentType, brandDB.Image!);
            }

            await context.SaveChangesAsync();
            return brandDB.ToDto();
        }
    }
}
