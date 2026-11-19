using System;
using System.Collections.Generic;

namespace TransporteUrbanoAPI.Models;

public partial class Asignacione
{
    public int CodigoAsignacion { get; set; }

    public int CodigoChofer { get; set; }

    public int NroCoche { get; set; }

    public DateOnly Fecha { get; set; }

    public TimeOnly HoraInicio { get; set; }

    public TimeOnly HoraFin { get; set; }

    public virtual Chofere CodigoChoferNavigation { get; set; } = null!;

    public virtual Colectivo NroCocheNavigation { get; set; } = null!;
}
