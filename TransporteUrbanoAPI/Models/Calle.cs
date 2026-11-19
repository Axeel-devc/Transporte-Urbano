using System;
using System.Collections.Generic;

namespace TransporteUrbanoAPI.Models;

public partial class Calle
{
    public int CodigoCalle { get; set; }

    public string NombreCalle { get; set; } = null!;

    public virtual ICollection<Parada> Parada { get; set; } = new List<Parada>();
}
