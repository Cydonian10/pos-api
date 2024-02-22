﻿using PuntoVenta.Dtos;
using PuntoVenta.Modules.CashRegister.Dtos;

namespace PuntoVenta.Modules.Sales.Dtos
{
    public class SaleDto
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Taxes { get; set; }
        public DateTime Date { get; set; }
        public int VaucherNumber { get; set; }
        public UserSaleDto? Customer { get; set; }
        public UserSaleDto? Employed { get; set; }
        public CashRegisterDto? CashRegister { get; set; }
        public List<SaleDetailDto>? Products { get; set; }
    }
}