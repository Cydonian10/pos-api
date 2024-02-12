using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PuntoVenta.Database;
using PuntoVenta.Database.Entidades;
using PuntoVenta.Dtos;

namespace PuntoVenta.Controllers
{
    [Route("api/unit")]
    [ApiController]
    public class UnitController : CustomBaseController
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public UnitController(DataContext context, IMapper mapper) : base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<UnitMeasurementDto>>> List()
        { 
            return await List<UnitMeasurement,UnitMeasurementDto>();
        }

        [HttpGet("{id:int}", Name = "ObtnerUnit")]
        public async Task<ActionResult<UnitMeasurementDto>> GetOne([FromRoute] int id)
        {
            return await GetOne<UnitMeasurement,UnitMeasurementDto>(id);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] UnitMeasurementCreateDto dto)
        {
            return await Create<UnitMeasurement,UnitMeasurementCreateDto,UnitMeasurementDto>(dto, "ObtnerUnit");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] UnitMeasurementCreateDto dto)
        {
            return await Update<UnitMeasurement, UnitMeasurementCreateDto>(id, dto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            return await Delete<UnitMeasurement>(id);
        }
        
    }
}
