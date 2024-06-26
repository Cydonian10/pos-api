﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly DataContext context;

        public CategoriesController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Authorize(Roles = "admin,vendedor")]
        public async Task<ActionResult<List<CategoryDto>>> List([FromQuery] PaginationDto pageDto)
        {
            var queryble = context.Categories.AsQueryable();

            await HttpContext.InsertarParametrosPaginar(queryble, pageDto.quantityRecordsPerPage);
            var categoriesDB = await queryble.Paginar(pageDto).ToListAsync();
            return categoriesDB.Select(x => x.ToDto()).ToList();
        }


        [HttpGet("{id:int}", Name = "ObtenerCategory")]
        [Authorize(Roles = "admin,vendedor")]
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

            if (categoryDB == null) { return NotFound(new {msg = $"La unidad con id:{id} no fue encontrada" }); }
            var categoryDTO = new CategoryWithProductsDto
            {
                Id = categoryDB.Category.Id,
                Name = categoryDB.Category.Name,
                Description = categoryDB.Category.Description,
                Products = categoryDB.Products.ToList(),
            };
            return Ok(categoryDTO);
        }

        [HttpGet("{nombre}/find", Name = "ObtenerCategoryPorNombre")]
        [Authorize(Roles = "admin,vendedor")]
        public async Task<ActionResult<CategoryDto>> GetOneName([FromRoute] string nombre)
        {
            var categoryDB = await context.Categories.FirstOrDefaultAsync(x => x.Name == nombre);

            if (categoryDB == null) { return NotFound(new { msg = $"La categoria con nombre:{nombre} no fue encontrada" }); }
          
            return categoryDB.ToDto();
        }

        [HttpGet("filter")]
        [Authorize(Roles = "admin,vendedor")]
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
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Create([FromBody] CreateCategoryDto dto)
        {
            var category = dto.ToEntity();
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
            var categoryDTO = category.ToDto();

            return new CreatedAtRouteResult("ObtenerCategory", new { id = category.Id }, categoryDTO);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<CategoryDto>> Update([FromRoute] int id, [FromBody] CreateCategoryDto dto)
        {
            var categoryDB = await context.Categories.FindAsync(id);
            if (categoryDB == null) { return NotFound(new { msg = $"La unidad con id:{id} no fue encontrada" }); }

            categoryDB.ToEntityUpdate(dto);
            await context.SaveChangesAsync();

            return categoryDB.ToDto();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<CategoryDto>> Delete([FromRoute] int id)
        {
            var categoryDB = await context.Categories.FindAsync(id);
            if (categoryDB == null) { return NotFound(new { msg = $"La unidad con id:{id} no fue encontrada" }); }

            context.Categories.Remove(categoryDB);
            await context.SaveChangesAsync();

            return Ok(new { id });
        }
    }
}
