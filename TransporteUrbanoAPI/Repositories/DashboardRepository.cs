using TransporteUrbanoAPI.Dtos;
using TransporteUrbanoAPI.Models;
using TransporteUrbanoAPI.Repositories.Interfaces;

namespace TransporteUrbanoAPI.Repositories;

public class DashboardRepository : IDashboardRepository
{
    private readonly TransporteUrbanoContext _context;

    public DashboardRepository(TransporteUrbanoContext context)
    {
        _context = context;
    }

    public DashboardDto GetResumen()
    {
        DateOnly hoy = DateOnly.FromDateTime(DateTime.Today);
        DateOnly hace6Meses = hoy.AddMonths(-6);
        DateOnly primerMes = new DateOnly(hoy.Year, hoy.Month, 1).AddMonths(-5);

        List<ItemCantidadDto> porEstado = _context.Colectivos
            .GroupBy(c => c.CodigoEstadoNavigation.Descripcion)
            .Select(g => new ItemCantidadDto { Nombre = g.Key, Cantidad = g.Count() })
            .ToList();

        List<ItemCantidadDto> porLinea = _context.Colectivos
            .GroupBy(c => c.CodigoLineaNavigation.NumeroLinea)
            .OrderBy(g => g.Key)
            .Select(g => new ItemCantidadDto { Nombre = "Línea " + g.Key, Cantidad = g.Count() })
            .ToList();

        // Es el NOT EXISTS de tu Consulta 1
        List<CocheSinMantenimientoDto> sinMantenimiento = _context.Colectivos
            .Where(c => !c.Mantenimientos.Any(m => m.Fecha >= hace6Meses))
            .OrderBy(c => c.NroCoche)
            .Select(c => new CocheSinMantenimientoDto
            {
                NroCoche = c.NroCoche,
                Patente = c.Patente,
                NumeroLinea = c.CodigoLineaNavigation.NumeroLinea
            })
            .ToList();

        var costos = _context.Mantenimientos
            .Where(m => m.Fecha >= primerMes)
            .Select(m => new { m.Fecha, Costo = m.Costo ?? 0m })
            .ToList();

        List<CostoMesDto> costoPorMes = Enumerable.Range(0, 6)
            .Select(i => primerMes.AddMonths(i))
            .Select(mes => new CostoMesDto
            {
                Mes = mes.ToString("MM/yyyy"),
                Total = costos.Where(c => c.Fecha.Year == mes.Year && c.Fecha.Month == mes.Month).Sum(c => c.Costo)
            })
            .ToList();

        return new DashboardDto
        {
            TotalColectivos = _context.Colectivos.Count(),
            MantenimientosUltimos6Meses = _context.Mantenimientos.Count(m => m.Fecha >= hace6Meses),
            CostoUltimos6Meses = _context.Mantenimientos.Where(m => m.Fecha >= hace6Meses).Sum(m => m.Costo ?? 0m),
            PorEstado = porEstado,
            PorLinea = porLinea,
            SinMantenimiento = sinMantenimiento,
            CostoPorMes = costoPorMes
        };
    }
}
