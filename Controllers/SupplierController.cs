//using AutoMapper;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using PuntoVenta.Database;
//using PuntoVenta.Database.Entidades;
//using PuntoVenta.Dtos;

//namespace PuntoVenta.Controllers
//{
//    [Route("api/supplier")]
//    [ApiController]
//    public class SupplierController : CustomBaseController
//    {
//        public DataContext Context { get; }
//        public IMapper Mapper { get; }

//        public SupplierController(DataContext context, IMapper mapper) : base(context, mapper)
//        {
//            Context = context;
//            Mapper = mapper;
//        }

//        [HttpGet]
//        public async Task<ActionResult<List<SupplierDto>>> List()
//        {
//            return await List<Supplier, SupplierDto>();
//        }

//        [HttpGet("{id:int}", Name = "ObtenerSupplier")]
//        public async Task<ActionResult<SupplierDto>> GetOne([FromRoute] int id)
//        {
//            return await GetOne<Supplier, SupplierDto>(id);
//        }

//        [HttpPost]
//        public async Task<ActionResult> Create([FromBody] SupplierDto dto)
//        {
//            return await Create<Supplier,SupplierDto,SupplierDto>(dto, "ObtenerSupplier");
//        }

//        [HttpPut("{id:int}")]
//        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] SupplierDto dto)
//        {
//            return await Update<Supplier,SupplierDto>(id,dto);
//        }

//        [HttpDelete("{id:int}")]
//        public async Task<ActionResult> Delete([FromRoute] int id)
//        {
//            return await Delete<Supplier>(id);
//        }
//    }
//}
