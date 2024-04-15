using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PuntoVenta.Database;
using PuntoVenta.Database.Entidades;
using PuntoVenta.Modules.Dashboard.Dtos;

namespace PuntoVenta.Modules.Dashboard
{
    [Route("api/dashboard")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Roles = "admin")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly DataContext context;

        public DashboardController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        //[Authorize(Policy = "VisualizarAdmin")]
        // Precio total del inventario al venderlo
        public async Task<ActionResult<DashboardDto>> GetDashboard()
        {
            var totalInventoryPrice = await context.Products.SumAsync(p => p.Stock * p.PurchasePrice);

            var totalInventoryCost = await context.Products.SumAsync(p => p.Stock * p.SalePrice);

            var totalSalesPrice = await context.Sales.SumAsync(p => p.TotalPrice);

            var totalProducts = await context.Products.CountAsync();

            return new DashboardDto
            {
                TotalInventoryCost = totalInventoryCost,
                TotalInventoryPrice = totalInventoryPrice,
                TotalSalesPrice = totalSalesPrice,
                TotalProducts = totalProducts,
            };
        }

    
        // Producto mas vendidos
        [HttpGet("most-selled")]
        public async Task<ActionResult> MostSelledProduct()
        {
            var top3SelledProduct = await context.Products
                .Include(x => x.UnitMeasurement)
                .OrderByDescending(p => p.QuantitySale)
                .Take(10)
                .Select(x => new DashboarProductDto
                {
                    Name = x.Name,
                    Stock = x.Stock,
                    QuantitySale = x.QuantitySale,
                    UnitSymbol = x.UnitMeasurement!.Symbol

                })
            .ToListAsync();

            return Ok(top3SelledProduct);
        }

        // Ventas por semana
        [HttpGet("sales-last-weak")]
        public async Task<ActionResult> SalesLastWeak()
        {
            DateTime fechaActual = DateTime.Today;
            DateTime fechaInicioSemanaPasada = fechaActual.AddDays(-7);

            var sales = await context.Sales
                .Where(sale => sale.Date >= fechaInicioSemanaPasada && sale.Date <= fechaActual).ToListAsync();

            var salesByDay = sales!.GroupBy(sale => sale.Date.DayOfWeek)
                .Select(group => new {
                    Dia = group.Key.ToString(),
                    TotalSale = group.Sum( sale => sale.TotalPrice)
                }).ToList();

            return Ok(salesByDay);
                  
        }

        [HttpGet("least-sold-product")]
        public async Task<ActionResult> LeastSoldProduct()
        {
            var top3leastSellingProducts = await context.Products
              .Include(x => x.UnitMeasurement)
              .OrderBy(p => p.QuantitySale)
              .Take(4)
              .Select(x => new DashboarProductDto
              {
                  Name = x.Name,
                  Stock = x.Stock,
                  QuantitySale = x.QuantitySale,
                  UnitSymbol = x.UnitMeasurement!.Symbol
              })
            .ToListAsync();

            return Ok(top3leastSellingProducts);
        }

        [HttpGet("top-sales")]
        public async Task<ActionResult> GetBestClients()
        {

            var topSales = await (from sale in context.Sales
                        join customer in context.Customers on sale.CustomerId equals customer.Id
                        group sale by new { sale.CustomerId, customer.Name } into g
                        orderby g.Sum(s => s.TotalPrice) descending
                        select new DashboardCustomerDto
                        {
                            CustomerId = g.Key.CustomerId,
                            UserName = g.Key.Name,
                            TotalSales = g.Sum(s => s.TotalPrice)
                        }).Take(3).ToListAsync();


            return Ok(topSales);    
        }


        [HttpGet("products-low-stock")]
        public async Task<ActionResult> ProductsLowInStock()
        {
            var productsOutOfStock = await context.Products
                .Include(x => x.UnitMeasurement)
                .Where(x => x.Stock < 30)
                .Select(x => new DashboarProductDto
                {
                    Name = x.Name,
                    Stock = x.Stock,
                    QuantitySale = x.QuantitySale,
                    UnitSymbol = x.UnitMeasurement!.Symbol
                })
                .OrderByDescending(x => x.Stock)
                .ToListAsync();

            return Ok(productsOutOfStock);
        }

    }
}

//--select Sum(Stock * PurchasePrice) as TotalInventoryPrice from Products;

//--select Sum(Stock * SalePrice) as TotalInventoryCost from Products;

//--select top(3) Name, TotalSales from Products order by TotalSales asc;

//--select top(3) Name, TotalSales from Products order by TotalSales desc;

//--select sum(TotalPrice) TotalSalePrice from sales;

//--select count(*) from Products;

//--select * from Products where Stock <= 20;


//--select top(2) sum(TotalPrice),CustomerId, us.Name from Sales 
//--	inner join AspNetUsers us on us.Id = sales.CustomerId Group by CustomerId, us.Name order by sum(TotalPrice) desc;