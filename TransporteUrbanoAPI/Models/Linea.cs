using System;
using System.Collections.Generic;

namespace TransporteUrbanoAPI.Models;

public partial class Linea
{
    public int CodigoLinea { get; set; }

    public string NumeroLinea { get; set; } = null!;

    public virtual ICollection<Colectivo> Colectivos { get; set; } = new List<Colectivo>();

    public virtual ICollection<Recorrido> Recorridos { get; set; } = new List<Recorrido>();
}
