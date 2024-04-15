using System.ComponentModel.DataAnnotations.Schema;

namespace PuntoVenta.Modules.Sales.Dtos
{
    public class BillsDto
    {
        public int Id { get; set; }
        public int VaucherNumber { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Taxes { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
        public DateTime Date { get; set; }
        public string? User { get; set; }
        public string? Customer { get; set; }
        public string? CashRegister { get; set; }
        public decimal TotalDescount => Productos!.Sum(x => x.Descount);
        public List<ProductDTO>? Productos { get; set; } 

    }

    public class ProductDTO
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal Quantity { get; set; }
        public string? Name { get; set; }
        public string? Categoria { get; set; }
        public string? Marca { get; set; }
        public string? Unit { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SubTotal { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Descount { get; set; }
    }
}
