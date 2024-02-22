using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PuntoVenta.Database;
using PuntoVenta.Database.Mappers;
using PuntoVenta.Modules.Sales.Dtos;
using System.Security.Claims;

namespace PuntoVenta.Modules.Sales
{
    [Route("api/sales")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SalesController : ControllerBase
    {
        private readonly DataContext context;

        public SalesController(DataContext context)
        {
            this.context = context;
        }

        [Authorize(Policy = "ManejadorVentas")]
        [HttpPost]
        public async Task<IActionResult> GenerateSale([FromBody] SaleCrearDto crearDto)
        {
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    var sale = crearDto.ToEntity();
                    var employedId = HttpContext.User.Claims.Where(claim => claim.Type == ClaimTypes.NameIdentifier).FirstOrDefault()!.Value;

                    sale.EmployedId = employedId;
                    sale.SaleDetails!.ForEach(x =>
                    {
                        x.SubTotal = Math.Round((decimal)(x.Quantity * x.UnitPrice)!, 2);

                        if (x.Descuento > 0)
                        {
                            var descuento = Math.Round((decimal)(x.SubTotal * x.Descuento / 100)!, 2);
                            x.SubTotal -= descuento;
                        }
                    });
                    sale.TotalPrice = sale.SaleDetails.Sum(x => x.SubTotal);

                    await context.Sales.AddAsync(sale);
                    await context.SaveChangesAsync();

                    foreach (var detail in sale.SaleDetails)
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

                    var cashRegisterDb = await context.CashRegisters.FirstOrDefaultAsync(x => x.Id == crearDto.CashRegisterId);

                    if (cashRegisterDb != null)
                    {
                        cashRegisterDb!.TotalCash += sale.TotalPrice;
                    }

                    await context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return NoContent();
                } catch (Exception ex)
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
                    .Include(x => x.CashRegister)
                    .Include(x => x.SaleDetails!)
                    .ThenInclude(x => x.Product)
                    .ThenInclude(x => x!.UnitMeasurement)
                    .OrderByDescending(x => x.Date)
                    .Take(10)
                    .ToListAsync();

            var saleDTO = ventasEntity.Select(x => x.ToDto()).ToList();

            return Ok(saleDTO);
        }


    }
}
