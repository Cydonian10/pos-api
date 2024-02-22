﻿using PuntoVenta.Database.Entidades;
using PuntoVenta.Modules.Categories.Dtos;
using PuntoVenta.Modules.Units.Dtos;
using System.ComponentModel.DataAnnotations.Schema;

namespace PuntoVenta.Modules.Products.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Stock { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SalePrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PurchasePrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PurchaseDesc { get; set; }
        public string? Image { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }
        public string? Size { get; set; }
        public string? CondicionDiscount { get; set; }
        public string? Name { get; set; }
        public virtual CategoryDto? Category { get; set; }
        public virtual UnitDto? UnitMeasurement { get; set; }
   
    }
}