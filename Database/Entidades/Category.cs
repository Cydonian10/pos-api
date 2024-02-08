﻿namespace PuntoVenta.Database.Entidades
{
    public class Category : IId
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<ProductName>? ProductNames { get; set; }
    }
}
