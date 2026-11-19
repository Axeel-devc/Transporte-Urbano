using System;
using System.Collections.Generic;

namespace TransporteUrbanoAPI.Models;

public partial class Repuesto
{
    public int CodigoRepuesto { get; set; }

    public string Descripcion { get; set; } = null!;

    public int CodigoTipoRepuesto { get; set; }

    public int CodigoProveedor { get; set; }

    public int StockMinimo { get; set; }

    public decimal? PrecioUnitario { get; set; }

    public virtual Proveedore CodigoProveedorNavigation { get; set; } = null!;

    public virtual TiposRepuesto CodigoTipoRepuestoNavigation { get; set; } = null!;

    public virtual ICollection<DetalleMantenimiento> DetalleMantenimientos { get; set; } = new List<DetalleMantenimiento>();

    public virtual ICollection<MovimientosStock> MovimientosStocks { get; set; } = new List<MovimientosStock>();
}
