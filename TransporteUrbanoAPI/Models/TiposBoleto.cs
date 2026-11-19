using System;
using System.Collections.Generic;

namespace TransporteUrbanoAPI.Models;

public partial class TiposBoleto
{
    public int CodigoTipoBoleto { get; set; }

    public string Descripcion { get; set; } = null!;

    public decimal Precio { get; set; }

    public virtual ICollection<Boleto> Boletos { get; set; } = new List<Boleto>();
}
