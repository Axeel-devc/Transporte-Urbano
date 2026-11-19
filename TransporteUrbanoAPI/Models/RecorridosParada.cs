using System;
using System.Collections.Generic;

namespace TransporteUrbanoAPI.Models;

public partial class RecorridosParada
{
    public int CodigoRecorrido { get; set; }

    public int CodigoParada { get; set; }

    public int Orden { get; set; }

    public virtual Parada CodigoParadaNavigation { get; set; } = null!;

    public virtual Recorrido CodigoRecorridoNavigation { get; set; } = null!;
}
