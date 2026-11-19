using System;
using System.Collections.Generic;

namespace TransporteUrbanoAPI.Models;

public partial class Colectivo
{
    public int NroCoche { get; set; }

    public string Patente { get; set; } = null!;

    public string? Marca { get; set; }

    public string? Modelo { get; set; }

    public int CodigoLinea { get; set; }

    public int CodigoEstado { get; set; }

    public virtual ICollection<Asignacione> Asignaciones { get; set; } = new List<Asignacione>();

    public virtual EstadosColectivo CodigoEstadoNavigation { get; set; } = null!;

    public virtual Linea CodigoLineaNavigation { get; set; } = null!;

    public virtual ICollection<Mantenimiento> Mantenimientos { get; set; } = new List<Mantenimiento>();

    public virtual ICollection<Servicio> Servicios { get; set; } = new List<Servicio>();
}
