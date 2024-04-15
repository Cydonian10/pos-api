using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PuntoVenta.Database;
using PuntoVenta.Database.Entidades;
using PuntoVenta.Database.Mappers;
using PuntoVenta.Modules.Purchases.Dtos;
using PuntoVenta.Modules.Sales.Dtos;

namespace PuntoVenta.Modules.Purchases
{
    [Route("api/purchases")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly DataContext context;

        public PurchasesController(DataContext context)
        {
            this.context = context;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Create([FromBody] CreatePurchaseDto crearPurchaseDto)
        {
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    var purchase = crearPurchaseDto.ToEntity();

                    purchase.VaucherNumber = purchase.VaucherNumber + 1;

                    // Use un bucle for para manejar operaciones asincrónicas de manera adecuada
                    foreach (var purchaseDetail in crearPurchaseDto.Details!)
                    {
                        var product = await context.Products.FirstOrDefaultAsync(p => p.Id == purchaseDetail.Product!.Id);

                        if (product != null)
                        {
                            product.Stock += purchaseDetail.Quantity;

                            if (product.PurchasePrice != purchaseDetail.PurchasePrice)
                            {
                                product.PurchasePrice = purchaseDetail.PurchasePrice;
                            }
                        }
                    }

                    purchase.TotalPrice = Math.Round(purchase.PurchaseDetails!.Sum(x => x.SubTotal),2);

                    await context.Purchases.AddAsync(purchase);
                    await context.SaveChangesAsync();

                    await transaction.CommitAsync();
                    return NoContent();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return StatusCode(500, new { Message = "Error al generar la venta", Msg = ex.Message });
                }
            }
        }

        [HttpGet()]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Filter([FromQuery] FilterPurchaseDto filtro)
        {
            var purchasesDB = await context.Purchases
                .Where(x => x.Date >= filtro.StartDate && x.Date <= filtro.FinalDate)
                .ToListAsync();

            return Ok(purchasesDB.Select(x => x.ToDto()).ToList());
        }


        [HttpGet("supplier/{supplierId:int}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> GetBySupplierAndDate([FromRoute] int supplierId, [FromQuery] FilterSaleByDate filterByDate)
        {

            var purchases = await context.Purchases
                     .Where(x => x.SupplierId == supplierId && x.Date.Date >= filterByDate.StarDate.Date && x.Date <= filterByDate.EndDate.Date)
                     .Select(x => new
                     {
                         x.Id,
                         x.Supplier!.Name,
                         x.TotalPrice,
                         Products = x.PurchaseDetails!.Select( x => new
                         {
                            x.Quantity,
                            x.SubTotal,
                           x.Product!.Name,
                           x.Product.PurchasePrice,
                           Brand = x.Product.Brand!.Name,
                           Unit = x.Product.UnitMeasurement!.Name
                            
                         })
                     })
                     .ToListAsync();


            return Ok(purchases);
        }
    }
}
