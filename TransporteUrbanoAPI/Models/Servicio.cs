using System;
using System.Collections.Generic;

namespace TransporteUrbanoAPI.Models;

public partial class Servicio
{
    public int CodigoServicio { get; set; }

    public int CodigoRecorrido { get; set; }

    public int NroCoche { get; set; }

    public int CodigoChofer { get; set; }

    public DateOnly Fecha { get; set; }

    public TimeOnly HoraInicio { get; set; }

    public TimeOnly HoraFin { get; set; }

    public virtual ICollection<Boleto> Boletos { get; set; } = new List<Boleto>();

    public virtual Chofere CodigoChoferNavigation { get; set; } = null!;

    public virtual Recorrido CodigoRecorridoNavigation { get; set; } = null!;

    public virtual Colectivo NroCocheNavigation { get; set; } = null!;

    public virtual ICollection<UbicacionCoche> UbicacionCoches { get; set; } = new List<UbicacionCoche>();
}
