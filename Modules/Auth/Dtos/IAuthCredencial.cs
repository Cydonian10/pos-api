namespace PuntoVenta.Modules.Auth.Dtos
{
    public interface IAuthCredencial
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
