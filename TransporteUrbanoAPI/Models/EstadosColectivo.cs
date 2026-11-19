using System;
using System.Collections.Generic;

namespace TransporteUrbanoAPI.Models;

public partial class EstadosColectivo
{
    public int CodigoEstado { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Colectivo> Colectivos { get; set; } = new List<Colectivo>();
}
