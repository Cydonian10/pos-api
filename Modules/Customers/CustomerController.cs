using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PuntoVenta.Database;
using PuntoVenta.Database.Entidades;
using PuntoVenta.Database.Mappers;
using PuntoVenta.Helpers.Base;
using PuntoVenta.Helpers.Dtos;
using PuntoVenta.Helpers.Extensions;
using PuntoVenta.Modules.Customers.Dtos;

namespace PuntoVenta.Modules.Customers
{
    [Route("api/customers")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class CustomerController : CustomControllerBase
    {
        private readonly DataContext context;

        public CustomerController(DataContext context) : base(context)
        {
            this.context = context;
        }

        [HttpGet]
        [Authorize(Roles = "admin,vendedor")]
        public async Task<ActionResult<List<CustomerDto>>> Get([FromQuery] PaginationDto paginationDto)
        {
            var queryble = context.Customers.AsQueryable();

            await HttpContext.InsertarParametrosPaginar(queryble, paginationDto.quantityRecordsPerPage);
            var customersDB = await queryble.Paginar(paginationDto).ToListAsync();

            return customersDB.Select(x => x.ToDto()).ToList();

        }


        [HttpGet("{id:int}", Name = "ObtenerCustomers")]
        [Authorize(Roles = "admin,vendedor")]
        public async Task<ActionResult<CustomerDto>> Get([FromRoute] int id)
        {
            var customerDB = await context.Customers.FirstOrDefaultAsync(x => x.Id == id);

            if (customerDB == null) { return NotFound(new {msg = $"El cliente con id: {id} no existe" }); }

            return customerDB.ToDto();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Create([FromBody] CreateCustomerDto createCustomerDto)
        {
            var customer = createCustomerDto.ToEntity();

            await context.Customers.AddAsync(customer);
            await context.SaveChangesAsync();

            return new CreatedAtRouteResult("ObtenerCustomers", new { id = customer.Id }, customer.ToDto());
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<CustomerDto>> Put([FromBody] CreateCustomerDto createCustomerDto, [FromRoute] int id)
        {
            var customerDB = await context.Customers.FirstOrDefaultAsync(x => x.Id == id);

            if (customerDB == null) { return NotFound(new { msg = $"Customer con id {id} no encontrado" }); }

            customerDB.ToEntityUpdate(createCustomerDto);

            await context.SaveChangesAsync();
            return customerDB.ToDto();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            return await Delete<Customer>(id);
        }

    }
}
