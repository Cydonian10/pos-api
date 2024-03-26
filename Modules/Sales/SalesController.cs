using ClosedXML.Excel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PuntoVenta.Database;
using PuntoVenta.Database.Entidades;
using PuntoVenta.Database.Mappers;
using PuntoVenta.Modules.Sales.Dtos;
using System.Data;
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
        public async Task<ActionResult> GenerateSale([FromBody] CreateSaleDto crearDto)
        {
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {


                    var sale = crearDto.ToEntity();
                    var employedId = HttpContext.User.Claims.Where(claim => claim.Type == ClaimTypes.NameIdentifier).FirstOrDefault()!.Value;

                    sale.EmployedId = employedId;
                    sale.Date = DateTime.Now;
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

                    if (crearDto.statusCompra == EStatusCompra.Cotizacion)
                    {

                        return GenerarExel(sale, crearDto.SaleDetails!);
                    }

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
                                product.QuantitySale += detail.Quantity;
                            }
                            else
                            {
                                // Lanzar una excepción personalizada si la cantidad a restar es mayor que el stock actual
                                throw new InvalidOperationException($"La cantidad a restar ({detail.Quantity}) es mayor que el stock actual ({product.Stock}) para el producto con ID {product.Id}.");
                            }
                        }
                    }

                    var cashRegisterDb = await context.CashRegisters.FirstOrDefaultAsync(x => x.Id == crearDto.CashRegisterId);

                    if (cashRegisterDb!.Open == false)
                    {
                        throw new InvalidOperationException($"La caja esta cerrada");
                    }

                    if (cashRegisterDb != null)
                    {
                        cashRegisterDb!.TotalCash += sale.TotalPrice;
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


        [HttpGet("my-sales")]
        public async Task<ActionResult> MySales()
        {
            var employedId = HttpContext.User.Claims.Where(claim => claim.Type == ClaimTypes.NameIdentifier).FirstOrDefault()!.Value;

            var salesDB = await context.Sales
                    .Include(x => x.Employed)
                    .Include(x => x.Customer)
                    .Include(x => x.CashRegister)
                    .Include(x => x.SaleDetails!)
                    .ThenInclude(x => x.Product)
                    .ThenInclude(x => x!.UnitMeasurement)
                    .Where(x => x.EmployedId == employedId)
                    .OrderByDescending(x => x.Date)
                    .Take(20)
                    .ToListAsync();

            var saleDTO = salesDB.Select(x => x.ToDto()).ToList();

            return Ok(saleDTO);
        }

        [HttpDelete("{id:int}/caja/{cajaId}")]
        public async Task<ActionResult> Delete([FromRoute] int id, [FromRoute] int cajaId)
        {
            var saleDb = await context.Sales.Include(x => x.SaleDetails).FirstOrDefaultAsync(x => x.Id == id);

            if (saleDb == null) { return NotFound(); }


            foreach (var detail in saleDb.SaleDetails!)
            {
                var product = await context.Products.FindAsync(detail.ProductId);

                if (product != null)
                {

                    product.Stock += detail.Quantity;
                    product.QuantitySale -= detail.Quantity;

                }
            }

            var cashRegisterDb = await context.CashRegisters.FirstOrDefaultAsync(x => x.Id == cajaId);

           
            if (cashRegisterDb != null)
            {
                cashRegisterDb!.TotalCash -= saleDb.TotalPrice;
            }

            context.Remove(saleDb);
            await context.SaveChangesAsync();

            return Ok(new { id });
        }

        private FileResult GenerarExel(Sale sale, List<CrearteSaleDetailDto> dto)
        {
            DataTable dataTable = new DataTable("Cotizacion");
            dataTable.Columns.AddRange(
            [
                new DataColumn("Nombre"),
                new DataColumn("Quantity",typeof(decimal)),
                new DataColumn("Unit Price",typeof(decimal)),
                new DataColumn("Sub Total",typeof(decimal)),
            ]);

            for (int i = 0; i < sale.SaleDetails!.Count(); i++)
            {
                dataTable.Rows.Add(dto[i].Name, dto[i].Quantity, dto[i].UnitPrice, sale.SaleDetails![i].SubTotal);
            }
            using (var memoryStream = new MemoryStream())
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add(dataTable, "Cotizacion");

                    worksheet.Cell(dataTable.Rows.Count + dto.Count + 1, 3).Value = "Total";
                    worksheet.Cell(dataTable.Rows.Count + dto.Count + 1, 4).Value = sale.TotalPrice;
                    workbook.SaveAs(memoryStream);

                    return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Cotizacion{Guid.NewGuid()}");
                }
            }
        }
    }
}
