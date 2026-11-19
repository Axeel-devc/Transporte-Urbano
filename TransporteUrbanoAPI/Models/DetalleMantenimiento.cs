using System;
using System.Collections.Generic;

namespace TransporteUrbanoAPI.Models;

public partial class DetalleMantenimiento
{
    public int CodigoDetalleMantenimiento { get; set; }

    public int CodigoMantenimiento { get; set; }

    public int CodigoRepuesto { get; set; }

    public int Cantidad { get; set; }

    public decimal CostoUnitario { get; set; }

    public virtual Mantenimiento CodigoMantenimientoNavigation { get; set; } = null!;

    public virtual Repuesto CodigoRepuestoNavigation { get; set; } = null!;
}
