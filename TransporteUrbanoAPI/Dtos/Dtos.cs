using System.ComponentModel.DataAnnotations;

namespace TransporteUrbanoAPI.Dtos;

public class LoginDto
{
    [Required(ErrorMessage = "Ingresá el usuario")]
    public string NombreUsuario { get; set; } = null!;
    [Required(ErrorMessage = "Ingresá la contraseña")]
    public string Password { get; set; } = null!;
}

public class LoginResponseDto
{
    public string Token { get; set; } = null!;
    public string Usuario { get; set; } = null!;
    public string Rol { get; set; } = null!;
    public DateTime Expira { get; set; }
}

public class ItemDto
{
    public int Id { get; set; }
    public string Descripcion { get; set; } = null!;
}

public class ColectivoDto
{
    public int NroCoche { get; set; }
    public string Patente { get; set; } = null!;
    public string? Marca { get; set; }
    public string? Modelo { get; set; }
    public int CodigoLinea { get; set; }
    public string NumeroLinea { get; set; } = null!;
    public int CodigoEstado { get; set; }
    public string Estado { get; set; } = null!;
}

public class ColectivoSaveDto
{
    [Required(ErrorMessage = "La patente es obligatoria")]
    [RegularExpression(@"^[A-Za-z]{2}\d{3}[A-Za-z]{2}$", ErrorMessage = "La patente debe tener formato AB123CD")]
    public string Patente { get; set; } = null!;
    [StringLength(50, ErrorMessage = "La marca admite hasta 50 caracteres")]
    public string? Marca { get; set; }
    [StringLength(50, ErrorMessage = "El modelo admite hasta 50 caracteres")]
    public string? Modelo { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Seleccioná una línea")]
    public int CodigoLinea { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Seleccioná un estado")]
    public int CodigoEstado { get; set; }
}

public class MantenimientoDto
{
    public int CodigoMantenimiento { get; set; }
    public int NroCoche { get; set; }
    public string Patente { get; set; } = null!;
    public int CodigoTipoMantenimiento { get; set; }
    public string TipoMantenimiento { get; set; } = null!;
    public DateOnly Fecha { get; set; }
    public string? Descripcion { get; set; }
    public decimal? Costo { get; set; }
}

public class MantenimientoSaveDto
{
    [Range(1, int.MaxValue, ErrorMessage = "Seleccioná un colectivo")]
    public int NroCoche { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Seleccioná un tipo de mantenimiento")]
    public int CodigoTipoMantenimiento { get; set; }
    public DateOnly Fecha { get; set; }
    [StringLength(250, ErrorMessage = "La descripción admite hasta 250 caracteres")]
    public string? Descripcion { get; set; }
    public decimal? Costo { get; set; }
}

public class ItemCantidadDto
{
    public string Nombre { get; set; } = null!;
    public int Cantidad { get; set; }
}

public class CostoMesDto
{
    public string Mes { get; set; } = null!;
    public decimal Total { get; set; }
}

public class CocheSinMantenimientoDto
{
    public int NroCoche { get; set; }
    public string Patente { get; set; } = null!;
    public string NumeroLinea { get; set; } = null!;
}

public class DashboardDto
{
    public int TotalColectivos { get; set; }
    public int MantenimientosUltimos6Meses { get; set; }
    public decimal CostoUltimos6Meses { get; set; }
    public List<ItemCantidadDto> PorEstado { get; set; } = new();
    public List<ItemCantidadDto> PorLinea { get; set; } = new();
    public List<CostoMesDto> CostoPorMes { get; set; } = new();
    public List<CocheSinMantenimientoDto> SinMantenimiento { get; set; } = new();
}
public class RegistroDto
{
    [Required(ErrorMessage = "Ingresá un nombre de usuario")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "El usuario debe tener entre 3 y 50 caracteres")]
    public string NombreUsuario { get; set; } = null!;

    [Required(ErrorMessage = "Ingresá tu email")]
    [EmailAddress(ErrorMessage = "El email no es válido")]
    [StringLength(100, ErrorMessage = "El email admite hasta 100 caracteres")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Ingresá una contraseña")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
    public string Password { get; set; } = null!;
}
