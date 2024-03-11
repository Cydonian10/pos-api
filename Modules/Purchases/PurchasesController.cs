using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PuntoVenta.Database;
using PuntoVenta.Database.Mappers;
using PuntoVenta.Modules.Purchases.Dtos;

namespace PuntoVenta.Modules.Purchases
{
    [Route("api/purchases")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly DataContext context;

        public PurchasesController(DataContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreatePurchaseDto crearPurchaseDto)
        {
            using(var transaction = await  context.Database.BeginTransactionAsync())
            {
                try
                {
                    var purchase = crearPurchaseDto.ToEntity();

                    purchase.PurchaseDetails!.ForEach( async x =>
                    {
                        var product = await context.Products.FirstOrDefaultAsync(p => p.Id == x.ProductId);

                        x.SubTotal = Math.Round((decimal)(x.Quantity * x.PurchasePrice), 2);

                        if (product != null)
                        {
                            product.Stock += x.Quantity;

                            if(product.PurchasePrice != x.PurchasePrice)
                            {
                                product.PurchasePrice = x.PurchasePrice;
                            }
                        }

                    });
                    purchase.TotalPrice = purchase.PurchaseDetails.Sum(x => x.SubTotal);

                    await context.Purchases.AddAsync(purchase);
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

        [HttpGet()]
        public async Task<ActionResult> Filter([FromQuery] FilterPurchaseDto filtro)
        {
            var purchasesDB = await context.Purchases
                .Where( x => x.Date >= filtro.StartDate && x.Date <= filtro.FinalDate  )
                .ToListAsync();

            return Ok(purchasesDB.Select(x => x.ToDto()).ToList());
        }
        
    }
}
