using System;
using System.Collections.Generic;

namespace TransporteUrbanoAPI.Models;

public partial class Usuario
{
    public int CodigoUsuario { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public DateTime? UltimoAcceso { get; set; }
}
