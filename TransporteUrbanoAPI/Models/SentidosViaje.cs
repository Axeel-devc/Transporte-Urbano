using System;
using System.Collections.Generic;

namespace TransporteUrbanoAPI.Models;

public partial class SentidosViaje
{
    public int CodigoSentido { get; set; }

    public string Sentido { get; set; } = null!;

    public virtual ICollection<Recorrido> Recorridos { get; set; } = new List<Recorrido>();
}
