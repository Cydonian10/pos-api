namespace PuntoVenta.Modules.Dashboard.Dtos
{
  public class DashboardDto
  {
    public decimal TotalInventoryPrice { get; set; }
    public decimal TotalInventoryCost { get; set; }
    public List<DashboarProductDto>? Top3SelledProduct { get; set; }
    public List<DashboarProductDto>? Top3leastSellingProducts { get; set; }
    public decimal TotalSalesPrice { get; set; }
    public int TotalProducts { get; set; }
    public List<DashboarProductDto>? ProductsOutOfStock { get; set; }
    public List<DashboardCustomerDto>? TopSales { get; set; }

  }
}

