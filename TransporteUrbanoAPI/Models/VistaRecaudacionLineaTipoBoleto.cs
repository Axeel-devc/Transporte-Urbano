using System;
using System.Collections.Generic;

namespace TransporteUrbanoAPI.Models;

public partial class VistaRecaudacionLineaTipoBoleto
{
    public string NumeroLinea { get; set; } = null!;

    public string TipoBoleto { get; set; } = null!;

    public int? CantidadBoletos { get; set; }

    public decimal? RecaudacionTotal { get; set; }
}
