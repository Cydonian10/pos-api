using ClosedXML.Excel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

        //[Authorize(Policy = "ManejadorVentas")]

        [HttpPost]
        [Authorize(Roles = "admin,vendedor")]
        public async Task<ActionResult> GenerateSale([FromBody] CreateSaleDto crearDto)
        {
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    var sale = crearDto.ToEntity();
                    var userId = HttpContext.User.Claims.Where(claim => claim.Type == ClaimTypes.NameIdentifier).FirstOrDefault()!.Value;

                    sale.userId = userId;
                    sale.VaucherNumber = sale.VaucherNumber + 1;

                    if (crearDto.StatusCompra == EStatusCompra.Cotizacion)
                    {

                        return GenerarExel(sale, crearDto.SaleDetails!);
                    }

                    await context.Sales.AddAsync(sale);

                    await context.SaveChangesAsync();

                    foreach (var detail in sale.SaleDetails!)
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
                                throw new InvalidOperationException($"La cantidad a restar ({detail.Quantity}) es mayor que el stock actual ({product.Stock}) para el producto {product.Name}.");
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
                    return Ok();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return StatusCode(500, new { Message = "Error al generar la venta", msg = ex.Message });
                }
            }

        }

        [HttpGet]
        [Authorize(Roles = "admin,vendedor")]
        public async Task<ActionResult<List<BillsDto>>> AllSales([FromQuery] FilterSaleByDate filterSaleByDate)
        {
            DateTime fechaInicio = new(2024, 04, 05); // Fecha de inicio del intervalo
            DateTime fechaFin = new(2024, 04, 06);

            var bills = await context.Sales
            .Where(v => v.Date.Date > filterSaleByDate.StarDate.Date && v.Date.Date <= filterSaleByDate.EndDate.Date)
            .Select(v => new BillsDto
            {
                Id = v.Id,
                VaucherNumber = v.VaucherNumber,
                Taxes = v.Taxes,
                TotalPrice = v.TotalPrice,
                Date = v.Date,
                Customer = v.Customer!.Name,
                CashRegister = v.CashRegister!.Name,
                User = v.User!.Name,
                Productos = v.SaleDetails!
                    .Where(d => d.SaleId == v.Id) // Filtramos los detalles de venta por la venta específica
                    .Select(d => new ProductDTO
                    {
                        Quantity = d.Quantity,
                        Name = d.Product!.Name,
                        Categoria = d.Product.Category!.Name,
                        Marca = d.Product.Brand!.Name,
                        SubTotal = d.SubTotal,
                        Unit = d.Product.UnitMeasurement!.Name,
                        Descount = (decimal)d.Descuento!
                    })
                    .ToList()
            })
            .ToListAsync();


            return bills;
        }

        [HttpGet("excel")]
        [Authorize(Roles = "admin,vendedor")]
        public async Task<ActionResult> ExportSaleExcel([FromQuery] FilterSaleByDate filterSaleByDate)
        {
          
            var nameFile = $"Sales.xlsx";

            var bills = await context.Sales
            .Where(v => v.Date.Date > filterSaleByDate.StarDate.Date && v.Date.Date <= filterSaleByDate.EndDate.Date)
            .Select(v => new BillsDto
            {
                Id = v.Id,
                VaucherNumber = v.VaucherNumber,
                Taxes = v.Taxes,
                TotalPrice = v.TotalPrice,
                Date = v.Date,
                Customer = v.Customer!.Name,
                CashRegister = v.CashRegister!.Name,
                User = v.User!.Name,
                Productos = v.SaleDetails!
                    .Where(d => d.SaleId == v.Id) // Filtramos los detalles de venta por la venta específica
                    .Select(d => new ProductDTO
                    {
                        Quantity = d.Quantity,
                        Name = d.Product!.Name,
                        Categoria = d.Product.Category!.Name,
                        Marca = d.Product.Brand!.Name,
                        SubTotal = d.SubTotal,
                        Unit = d.Product.UnitMeasurement!.Name,
                        Descount = (decimal)d.Descuento!
                    })
                    .ToList()
            })
            .ToListAsync();

            return GenerateExcelAllSaleByDate(nameFile, bills);
        }

        // cerrar caja
        [HttpDelete("{id:int}/caja/{cajaId}")]
        [Authorize(Roles = "admin,vendedor")]
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

        private FileResult GenerateExcelAllSaleByDate(string fileName, List<BillsDto> bills) 
        {
            DataTable dataTable = new DataTable("Sales");

            dataTable.Columns.AddRange([
                   new DataColumn("Id",typeof(int)),
                   new DataColumn("Fecha"),
                   new DataColumn("Empleado"),
                   new DataColumn("Cliente"),
                   new DataColumn("Precio Total",typeof(decimal)),
            ]);

            foreach (var bill  in bills)
            {
                dataTable.Rows.Add(bill.Id, bill.Date.ToString("yyyy-MM-dd"), bill.User,bill.Customer, bill.TotalPrice);
            }

            using var memoryStream = new MemoryStream();
            using var workBook = new XLWorkbook();
            var workSheet = workBook.Worksheets.Add(dataTable, "Sales");
            workSheet.ColumnsUsed().AdjustToContents();

            workSheet.Cell(dataTable.Rows.Count + bills.Count, 4).Value = "Total";
            workSheet.Cell(dataTable.Rows.Count + bills.Count, 5).Value = bills.Sum(x => x.TotalPrice);

            workBook.SaveAs(memoryStream);

            return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"${fileName}{Guid.NewGuid()}");


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
                dataTable.Rows.Add(dto[i].Product!.Name, dto[i].Quantity, dto[i].Product!.SalePrice, sale.SaleDetails![i].SubTotal);
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
