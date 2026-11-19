using Microsoft.EntityFrameworkCore;
using TransporteUrbanoAPI.Models;
using TransporteUrbanoAPI.Repositories.Interfaces;

namespace TransporteUrbanoAPI.Repositories;

public class MantenimientoRepository : IMantenimientoRepository
{
    private readonly TransporteUrbanoContext _context;

    public MantenimientoRepository(TransporteUrbanoContext context)
    {
        _context = context;
    }

    public List<Mantenimiento> GetAll(int? nroCoche)
    {
        IQueryable<Mantenimiento> query = _context.Mantenimientos
            .Include(m => m.NroCocheNavigation)
            .Include(m => m.CodigoTipoMantenimientoNavigation);

        if (nroCoche.HasValue)
            query = query.Where(m => m.NroCoche == nroCoche.Value);

        return query.OrderByDescending(m => m.Fecha).ThenByDescending(m => m.CodigoMantenimiento).ToList();
    }

    public Mantenimiento? GetById(int codigoMantenimiento)
    {
        return _context.Mantenimientos
            .Include(m => m.NroCocheNavigation)
            .Include(m => m.CodigoTipoMantenimientoNavigation)
            .FirstOrDefault(m => m.CodigoMantenimiento == codigoMantenimiento);
    }

    public Mantenimiento Add(Mantenimiento mantenimiento)
    {
        _context.Mantenimientos.Add(mantenimiento);
        _context.SaveChanges();
        return mantenimiento;
    }

    public void Update(Mantenimiento mantenimiento)
    {
        _context.Mantenimientos.Update(mantenimiento);
        _context.SaveChanges();
    }

    // Como DeleteViaje del ejemplo: primero el detalle, despues el maestro
    public void Delete(int codigoMantenimiento)
    {
        Mantenimiento? mantenimiento = _context.Mantenimientos
            .Include(m => m.DetalleMantenimientos)
            .FirstOrDefault(m => m.CodigoMantenimiento == codigoMantenimiento);

        if (mantenimiento != null)
        {
            _context.DetalleMantenimientos.RemoveRange(mantenimiento.DetalleMantenimientos);
            _context.Mantenimientos.Remove(mantenimiento);
            _context.SaveChanges();
        }
    }
}