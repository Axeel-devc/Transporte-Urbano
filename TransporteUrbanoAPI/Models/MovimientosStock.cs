using System;
using System.Collections.Generic;

namespace TransporteUrbanoAPI.Models;

public partial class MovimientosStock
{
    public int CodigoMovimiento { get; set; }

    public int CodigoRepuesto { get; set; }

    public DateTime Fecha { get; set; }

    public string TipoMovimiento { get; set; } = null!;

    public int Cantidad { get; set; }

    public string? Observacion { get; set; }

    public virtual Repuesto CodigoRepuestoNavigation { get; set; } = null!;
}
