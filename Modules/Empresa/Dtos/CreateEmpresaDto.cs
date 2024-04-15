namespace PuntoVenta.Modules.Empresa.Dtos
{
    public class CreateEmpresaDto
    {
        public string? Name { get; set; }
        public string? RUC { get; set; }
        public string? Address { get; set; }
        public IFormFile? Image { get; set; }
    }
}
