using System;
using System.Collections.Generic;

namespace TransporteUrbanoAPI.Models;

public partial class FormasPago
{
    public int CodigoFormaPago { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Boleto> Boletos { get; set; } = new List<Boleto>();
}
