using System;
using System.Collections.Generic;

namespace TransporteUrbanoAPI.Models;

public partial class Boleto
{
    public int CodigoBoleto { get; set; }

    public int CodigoServicio { get; set; }

    public int CodigoTipoBoleto { get; set; }

    public DateTime FechaHora { get; set; }

    public decimal Importe { get; set; }

    public int CodigoFormaPago { get; set; }

    public virtual FormasPago CodigoFormaPagoNavigation { get; set; } = null!;

    public virtual Servicio CodigoServicioNavigation { get; set; } = null!;

    public virtual TiposBoleto CodigoTipoBoletoNavigation { get; set; } = null!;
}
