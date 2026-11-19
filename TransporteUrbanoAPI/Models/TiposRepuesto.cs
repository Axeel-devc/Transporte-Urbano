using System;
using System.Collections.Generic;

namespace TransporteUrbanoAPI.Models;

public partial class TiposRepuesto
{
    public int CodigoTipoRepuesto { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Repuesto> Repuestos { get; set; } = new List<Repuesto>();
}
