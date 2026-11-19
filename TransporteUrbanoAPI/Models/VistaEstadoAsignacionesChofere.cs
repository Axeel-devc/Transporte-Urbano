using System;
using System.Collections.Generic;

namespace TransporteUrbanoAPI.Models;

public partial class VistaEstadoAsignacionesChofere
{
    public int CodigoChofer { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public int? CantidadAsignaciones { get; set; }

    public int? MinutosAsignados { get; set; }
}
