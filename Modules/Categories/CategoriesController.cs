using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PuntoVenta.Database;
using PuntoVenta.Database.Mappers;
using PuntoVenta.Helpers.Dtos;
using PuntoVenta.Helpers.Extensions;
using PuntoVenta.Modules.Categories.Dtos;
using PuntoVenta.Modules.Products.Dtos;

namespace PuntoVenta.Modules.Categories
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly DataContext context;

        public CategoriesController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> List([FromQuery] PageDto pageDto)
        {
            var queryble = context.Categories.AsQueryable();

            await HttpContext.InsertarParametrosPaginar(queryble, pageDto.quantityRecordsPerPage);
            var categoriesDB = await queryble.Paginar(pageDto).ToListAsync();
            return categoriesDB.Select(x => x.ToDto()).ToList();
        }

        [HttpGet("{id:int}", Name = "ObtenerCategory")]
        public async Task<ActionResult> GetOne([FromRoute] int id)
        {
            var categoryDB = await context.Categories
                    .Where(x => x.Id == id)
                    .Select(x => new
                    {
                        Category = x,
                        Products = x.Products!.Select(p => new CategoryProductsDto {
                           Id = p.Id,
                           Name = p.Name,
                           Stock = p.Stock
                        })
                    })
                    .FirstOrDefaultAsync();

            if (categoryDB == null) { return NotFound($"La unidad con id:{id} no fue encontrada"); }
            var categoryDTO = new CategoryWithProductsDto
            {
                Id = categoryDB.Category.Id,
                Name = categoryDB.Category.Name,
                Description = categoryDB.Category.Description,
                Products = categoryDB.Products.ToList(),
            };
            return Ok(categoryDTO);
        }


        [HttpGet("filter")]
        public async Task<ActionResult<List<CategoryDto>>> Filter([FromQuery] FilterCategoryDto dto)
        {
            var categoryQueryble = context.Categories.AsQueryable();

            if (!string.IsNullOrEmpty(dto.Name))
            {
                categoryQueryble = categoryQueryble.Where(x => x.Name!.Contains(dto.Name));
            }

            var categoriesDb = await categoryQueryble.ToListAsync();
            return categoriesDb.Select(x => x.ToDto()).ToList();
        }


        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateCategoryDto dto)
        {
            var category = dto.ToEntity();
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
            var categoryDTO = category.ToDto();

            return new CreatedAtRouteResult("ObtenerCategory", new { id = category.Id }, categoryDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<CategoryDto>> Update([FromRoute] int id, [FromBody] CreateCategoryDto dto)
        {
            var categoryDB = await context.Categories.FindAsync(id);
            if (categoryDB == null) { return NotFound($"La unidad con id:{id} no fue encontrada"); }

            categoryDB.ToEntityUpdate(dto);
            await context.SaveChangesAsync();

            return categoryDB.ToDto();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CategoryDto>> Delete([FromRoute] int id)
        {
            var categoryDB = await context.Categories.FindAsync(id);
            if (categoryDB == null) { return NotFound($"La unidad con id:{id} no fue encontrada"); }

            context.Categories.Remove(categoryDB);
            await context.SaveChangesAsync();

            return Ok(new { id });
        }
    }
}
