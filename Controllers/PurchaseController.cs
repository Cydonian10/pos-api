using AutoMapper;
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
    [Route("api/purchase")]
    [ApiController]
    public class PurchaseController : CustomBaseController
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public PurchaseController(DataContext context, IMapper mapper) : base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> GeneratePurchase([FromBody] PurchaseCrearDto dto)
        {
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    var purchaseEntity = mapper.Map<Purchase>(dto);

                    purchaseEntity!.TotalPrice = purchaseEntity.PurchaseDetails!.Sum(x => x.SubTotal);

                    await context.Purchases.AddAsync(purchaseEntity);    
                    await context.SaveChangesAsync();

                    foreach (var detail in purchaseEntity.PurchaseDetails!)
                    {
                        var product = await context.Products.FindAsync(detail.ProductId);
                        if (product != null)
                        {
                            product.Stock += detail.Quantity;
                        }
                        else
                        {
                            // Lanzar una excepción personalizada si la cantidad a restar es mayor que el stock actual
                            throw new InvalidOperationException($"La cantidad a restar ({detail.Quantity}) es mayor que el stock actual ({product!.Stock}) para el producto con ID {product.Id}.");
                        }
                    }

                    await context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return StatusCode(500, new { Message = "Error al generar la venta", Error = ex.Message });
                }
            }
        }

        [HttpGet]
        public async Task<ActionResult> AllPurchase()
        {
            var purchaseDb = await context.Purchases
                    .Include(x => x.Supplier)
                    .Include(x => x.PurchaseDetails!)
                    .ThenInclude(x => x.Product)
                    .ThenInclude(x => x!.UnitMeasurement)
                    .OrderByDescending(x => x.Date)
                    .ToListAsync();

            var purchaseDto = mapper.Map<List<PurchaseDto>>(purchaseDb);

            return Ok(purchaseDto);
        }
    }
}
