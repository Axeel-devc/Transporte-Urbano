using System;
using System.Collections.Generic;

namespace TransporteUrbanoAPI.Models;

public partial class Proveedore
{
    public int CodigoProveedor { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Telefono { get; set; }

    public virtual ICollection<Repuesto> Repuestos { get; set; } = new List<Repuesto>();
}
