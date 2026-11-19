using Microsoft.EntityFrameworkCore;
using TransporteUrbanoAPI.Models;
using TransporteUrbanoAPI.Repositories.Interfaces;

namespace TransporteUrbanoAPI.Repositories;

public class ColectivoRepository : IColectivoRepository
{
    private readonly TransporteUrbanoContext _context;

    public ColectivoRepository(TransporteUrbanoContext context)
    {
        _context = context;
    }

    public List<Colectivo> GetAll(int? codigoLinea, int? codigoEstado)
    {
        IQueryable<Colectivo> query = _context.Colectivos
            .Include(c => c.CodigoLineaNavigation)
            .Include(c => c.CodigoEstadoNavigation);

        if (codigoLinea.HasValue)
            query = query.Where(c => c.CodigoLinea == codigoLinea.Value);
        if (codigoEstado.HasValue)
            query = query.Where(c => c.CodigoEstado == codigoEstado.Value);

        return query.OrderBy(c => c.NroCoche).ToList();
    }

    public Colectivo? GetById(int nroCoche)
    {
        return _context.Colectivos
            .Include(c => c.CodigoLineaNavigation)
            .Include(c => c.CodigoEstadoNavigation)
            .FirstOrDefault(c => c.NroCoche == nroCoche);
    }

    public bool ExistePatente(string patente, int? excluirNroCoche)
    {
        return _context.Colectivos.Any(c => c.Patente == patente
            && (excluirNroCoche == null || c.NroCoche != excluirNroCoche));
    }

    public bool TieneRegistrosAsociados(int nroCoche)
    {
        return _context.Mantenimientos.Any(m => m.NroCoche == nroCoche)
            || _context.Servicios.Any(s => s.NroCoche == nroCoche)
            || _context.Asignaciones.Any(a => a.NroCoche == nroCoche);
    }

    public Colectivo Add(Colectivo colectivo)
    {
        _context.Colectivos.Add(colectivo);
        _context.SaveChanges();
        return colectivo;
    }

    public void Update(Colectivo colectivo)
    {
        _context.Colectivos.Update(colectivo);
        _context.SaveChanges();
    }

    public void Delete(int nroCoche)
    {
        Colectivo? colectivo = _context.Colectivos.Find(nroCoche);
        if (colectivo != null)
        {
            _context.Colectivos.Remove(colectivo);
            _context.SaveChanges();
        }
    }
}