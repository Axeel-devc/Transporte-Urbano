using System;
using System.Collections.Generic;

namespace TransporteUrbanoAPI.Models;

public partial class TiposMantenimiento
{
    public int CodigoTipoMantenimiento { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Mantenimiento> Mantenimientos { get; set; } = new List<Mantenimiento>();
}
