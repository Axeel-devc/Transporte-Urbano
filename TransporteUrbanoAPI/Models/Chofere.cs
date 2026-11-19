using System;
using System.Collections.Generic;

namespace TransporteUrbanoAPI.Models;

public partial class Chofere
{
    public int CodigoChofer { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Dni { get; set; } = null!;

    public virtual ICollection<Asignacione> Asignaciones { get; set; } = new List<Asignacione>();

    public virtual ICollection<Servicio> Servicios { get; set; } = new List<Servicio>();
}
