using System;
using System.Collections.Generic;

namespace TransporteUrbanoAPI.Models;

public partial class UbicacionCoche
{
    public int CodigoUbicacionCoche { get; set; }

    public int CodigoServicio { get; set; }

    public decimal? Latitud { get; set; }

    public decimal? Longitud { get; set; }

    public DateTime FechaHora { get; set; }

    public decimal? VelocidadKmxH { get; set; }

    public virtual Servicio CodigoServicioNavigation { get; set; } = null!;
}
