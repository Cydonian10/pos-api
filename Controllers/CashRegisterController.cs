using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PuntoVenta.Database;
using PuntoVenta.Database.Entidades;
using PuntoVenta.Dtos;

namespace PuntoVenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CashRegisterController : CustomBaseController
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public CashRegisterController(DataContext context, IMapper mapper) : base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CashRegisterDto>>> List()
        {
            return await List<CashRegister,CashRegisterDto>();
        }

        [HttpGet("{id:int}",Name = "ObtnerCashRegister")]
        public async Task<ActionResult<CashRegisterDto>> GetOne([FromRoute] int id)
        {
            return await GetOne<CashRegister,CashRegisterDto>(id);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CashRegisterCrearDto dto)
        {
            return await Create<CashRegister,CashRegisterCrearDto,CashRegisterDto>(dto, "ObtnerCashRegister");
        }
    }
}
