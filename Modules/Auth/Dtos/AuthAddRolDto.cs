﻿namespace PuntoVenta.Modules.Auth.Dtos
{
    public class AuthAddRolDto
    {
        public string? Email { get; set; }
        public string[]? Roles { get; set; }
    }
}