using System;
using System.Collections.Generic;

namespace TransporteUrbanoAPI.Models;

public partial class Parada
{
    public int CodigoParada { get; set; }

    public string? Descripcion { get; set; }

    public int CodigoCalle { get; set; }

    public string? NumeroCalle { get; set; }

    public virtual Calle CodigoCalleNavigation { get; set; } = null!;

    public virtual ICollection<RecorridosParada> RecorridosParada { get; set; } = new List<RecorridosParada>();
}
