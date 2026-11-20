using TransporteUrbanoAPI.Dtos;
using TransporteUrbanoAPI.Models;
using TransporteUrbanoAPI.Repositories.Interfaces;

namespace TransporteUrbanoAPI.Repositories;

public class CatalogoRepository : ICatalogoRepository
{
    private readonly TransporteUrbanoContext _context;

    public CatalogoRepository(TransporteUrbanoContext context)
    {
        _context = context;
    }

    public List<ItemDto> GetLineas()
    {
        return _context.Lineas
            .OrderBy(l => l.NumeroLinea)
            .Select(l => new ItemDto { Id = l.CodigoLinea, Descripcion = "Línea " + l.NumeroLinea })
            .ToList();
    }

    public List<ItemDto> GetEstados()
    {
        return _context.EstadosColectivos
            .OrderBy(e => e.CodigoEstado)
            .Select(e => new ItemDto { Id = e.CodigoEstado, Descripcion = e.Descripcion })
            .ToList();
    }

    public List<ItemDto> GetTiposMantenimiento()
    {
        return _context.TiposMantenimientos
            .OrderBy(t => t.Descripcion)
            .Select(t => new ItemDto { Id = t.CodigoTipoMantenimiento, Descripcion = t.Descripcion })
            .ToList();
    }
    public int? GetCodigoEstado(string descripcion) => _context.EstadosColectivos
        .Where(e => e.Descripcion == descripcion)
        .Select(e => (int?)e.CodigoEstado)
        .FirstOrDefault();

    public bool ExisteLinea(int id) => _context.Lineas.Any(l => l.CodigoLinea == id);
    public bool ExisteEstado(int id) => _context.EstadosColectivos.Any(e => e.CodigoEstado == id);
    public bool ExisteTipoMantenimiento(int id) => _context.TiposMantenimientos.Any(t => t.CodigoTipoMantenimiento == id);
}