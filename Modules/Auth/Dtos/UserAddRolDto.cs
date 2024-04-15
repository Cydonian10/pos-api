namespace PuntoVenta.Modules.Auth.Dtos
{
    public class UserAddRolDto
    {
        public string? Email { get; set; }
        public string[]? Roles { get; set; }
    }
}
