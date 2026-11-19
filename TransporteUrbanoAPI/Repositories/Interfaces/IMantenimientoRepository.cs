using TransporteUrbanoAPI.Models;

namespace TransporteUrbanoAPI.Repositories.Interfaces;

public interface IMantenimientoRepository
{
    List<Mantenimiento> GetAll(int? nroCoche);
    Mantenimiento? GetById(int codigoMantenimiento);
    Mantenimiento Add(Mantenimiento mantenimiento);
    void Update(Mantenimiento mantenimiento);
    void Delete(int codigoMantenimiento);
}