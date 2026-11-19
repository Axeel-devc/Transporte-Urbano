using System;
using System.Collections.Generic;

namespace TransporteUrbanoAPI.Models;

public partial class Recorrido
{
    public int CodigoRecorrido { get; set; }

    public string? Descripcion { get; set; }

    public int CodigoLinea { get; set; }

    public int CodigoSentido { get; set; }

    public virtual Linea CodigoLineaNavigation { get; set; } = null!;

    public virtual SentidosViaje CodigoSentidoNavigation { get; set; } = null!;

    public virtual ICollection<RecorridosParada> RecorridosParada { get; set; } = new List<RecorridosParada>();

    public virtual ICollection<Servicio> Servicios { get; set; } = new List<Servicio>();
}
