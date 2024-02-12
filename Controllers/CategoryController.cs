using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PuntoVenta.Database;
using PuntoVenta.Database.Entidades;
using PuntoVenta.Dtos;

namespace PuntoVenta.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : CustomBaseController
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public CategoryController(DataContext context, IMapper mapper) : base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> Get() 
        {
            return await List<Category,CategoryDto>();
        }


        [HttpGet("{id:int}",Name = "ObtenerCategory")]
        public async Task<ActionResult<CategoryDto>> GetOne([FromRoute] int id)
        {
            return await GetOne<Category, CategoryDto>(id);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CategoryCrearDto categoryCrearDto)
        {
            return await Create<Category, CategoryCrearDto,CategoryDto>(categoryCrearDto, "ObtenerCategory");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] CategoryCrearDto dto)
        {
            return await Update<Category, CategoryCrearDto>(id, dto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            return await Delete<Category>(id);
        }


    }
}
