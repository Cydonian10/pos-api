using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PuntoVenta.Database;
using PuntoVenta.Database.Entidades;
using PuntoVenta.Dtos;
using System.Security.Claims;

namespace PuntoVenta.Controllers
{
    [Route("api/sale")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class SaleController : CustomBaseController
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public SaleController(DataContext context, IMapper mapper) : base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [Authorize(Policy = "ManejadorVentas")]
        [HttpPost]
        public async Task<IActionResult> GenerateSale([FromBody] SaleCrearDto crearDto)
        {
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    var ventaEntity = mapper.Map<Sale>(crearDto);
                    var employedId = HttpContext.User.Claims.Where(claim => claim.Type == ClaimTypes.NameIdentifier).FirstOrDefault()!.Value;

                    ventaEntity!.EmployedId = employedId;
                    ventaEntity.SaleDetails!.ForEach(x =>
                    {
                        x.SubTotal = Math.Round((decimal)(x.Quantity * x.UnitPrice)!, 2);

                        if (x.Descuento > 0)
                        {
                            var descuento = Math.Round((decimal)(x.SubTotal * x.Descuento / 100)!, 2);
                            x.SubTotal -= descuento;
                        }
                    });
                    ventaEntity.TotalPrice = ventaEntity.SaleDetails.Sum(x => x.SubTotal);

                    await context.Sales.AddAsync(ventaEntity);
                    await context.SaveChangesAsync();

                    foreach (var detail in ventaEntity.SaleDetails)
                    {
                        var product = await context.Products.FindAsync(detail.ProductId);

                        if (product != null)
                        {
                            if (detail.Quantity <= product.Stock)
                            {
                                product.Stock -= detail.Quantity;
                            }
                            else
                            {
                                // Lanzar una excepción personalizada si la cantidad a restar es mayor que el stock actual
                                throw new InvalidOperationException($"La cantidad a restar ({detail.Quantity}) es mayor que el stock actual ({product.Stock}) para el producto con ID {product.Id}.");
                            }
                        }
                    }

                    

                    await context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return NoContent();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return StatusCode(500, new { Message = "Error al generar la venta", Error = ex.Message });
                }
            }
        }

        [HttpGet]
        [Authorize(Policy = "ManejadorVentas")]
        public async Task<ActionResult> AllSales()
        {
            
            var ventasEntity = await context.Sales
                    .Include(x => x.Employed)
                    .Include(x => x.Customer)
                    .Include(x => x.SaleDetails!)
                    .ThenInclude(x => x.Product)
                    .ThenInclude(x => x!.UnitMeasurement)
                    .OrderByDescending(x => x.Date)
                    .Take(10)
                    .ToListAsync();

            var ventasDto = mapper.Map<List<SaleDto>>(ventasEntity);

            return Ok(ventasDto);
        }



    }
}
