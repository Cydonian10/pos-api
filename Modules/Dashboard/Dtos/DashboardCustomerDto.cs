﻿namespace PuntoVenta.Modules.Dashboard.Dtos
{
    public class DashboardCustomerDto
    {
        public string? CustomerId { get; set; }
        public string? UserName { get; set; }
        public decimal TotalSales { get; set; }
    }
}
