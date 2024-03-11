using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PuntoVenta.Database;
using PuntoVenta.Database.Entidades;
using PuntoVenta.Modules.Dashboard.Dtos;

namespace PuntoVenta.Modules.Dashboard
{
    [Route("api/dashboard")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly DataContext context;

        public DashboardController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        // Precio total del inventario al venderlo
        public async Task<ActionResult<DashboardDto>> GetDashboard()
        {
            var totalInventoryPrice = await context.Products.SumAsync(p => p.Stock * p.PurchasePrice);

            var totalInventoryCost = await context.Products.SumAsync(p => p.Stock * p.SalePrice);

            var totalSalesPrice = await context.Sales.SumAsync(p => p.TotalPrice);

            var totalProducts = await context.Products.CountAsync();

            var productsOutOfStock = await context.Products
                .Where(x => x.Stock < 30)
                .Select(x => new DashboarProductDto
                {
                   Name = x.Name,
                   Stock = x.Stock,
                   TotalSales = x.TotalSales,
                })
                .ToListAsync();

            var top3SelledProduct = await context.Products
                .OrderByDescending(p => p.TotalSales)
                .Take(4)
                .Select(x => new DashboarProductDto
                {
                  Name = x.Name,
                  Stock = x.Stock,
                  TotalSales = x.TotalSales
                })
            .ToListAsync();

            var top3leastSellingProducts = await context.Products
              .OrderBy(p => p.TotalSales)
              .Take(4)
              .Select(x => new DashboarProductDto
              {
                Name = x.Name,
                Stock =  x.Stock,
                TotalSales = x.TotalSales
              })
            .ToListAsync();

            var query = from sale in context.Sales
                        join user in context.Users on sale.CustomerId equals user.Id
                        group sale by new { sale.CustomerId, user.Name } into g
                        orderby g.Sum(s => s.TotalPrice) descending
                        select new DashboardCustomerDto
                        {
                            CustomerId = g.Key.CustomerId,
                            UserName = g.Key.Name,
                            TotalSales = g.Sum(s => s.TotalPrice)
                        };

            var topSales = query.Take(3).ToList();

            return new DashboardDto
            {
                TotalInventoryCost = totalInventoryCost,
                TotalInventoryPrice = totalInventoryPrice,
                ProductsOutOfStock = productsOutOfStock,
                Top3SelledProduct = top3SelledProduct,
                Top3leastSellingProducts = top3leastSellingProducts,
                TotalSalesPrice = totalSalesPrice,
                TotalProducts = totalProducts,
                TopSales = topSales

            };
        }

        // Costo total del inventario
        //public async Task<ActionResult> GetInventoryCost()
        //{
        //    throw new NotImplementedException();
        //}

        //// Producto mas vendidos
        //public async Task<ActionResult> MostSelledProduct()
        //{
        //    throw new NotImplementedException();
        //}

        //// Productos menos vendidos
        //public async Task<ActionResult> LeastSoldProduct()
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<ActionResult> TotalSales()
        //{
        //    throw new NotImplementedException();

        //}

        //public async Task<ActionResult> TotalProducts()
        //{
        //    throw new NotImplementedException();

        //}

        //public async Task<ActionResult> GetBestClients()
        //{
        //    throw new NotImplementedException();

        //}


        //public async Task<ActionResult> ProductsLowInStock()
        //{
        //    throw new NotImplementedException();
        //}

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