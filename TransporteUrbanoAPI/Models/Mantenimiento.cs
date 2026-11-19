using System;
using System.Collections.Generic;

namespace TransporteUrbanoAPI.Models;

public partial class Mantenimiento
{
    public int CodigoMantenimiento { get; set; }

    public int NroCoche { get; set; }

    public int CodigoTipoMantenimiento { get; set; }

    public DateOnly Fecha { get; set; }

    public string? Descripcion { get; set; }

    public decimal? Costo { get; set; }

    public virtual TiposMantenimiento CodigoTipoMantenimientoNavigation { get; set; } = null!;

    public virtual ICollection<DetalleMantenimiento> DetalleMantenimientos { get; set; } = new List<DetalleMantenimiento>();

    public virtual Colectivo NroCocheNavigation { get; set; } = null!;
}
