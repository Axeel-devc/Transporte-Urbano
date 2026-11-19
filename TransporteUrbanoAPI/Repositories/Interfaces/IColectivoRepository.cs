using TransporteUrbanoAPI.Models;

namespace TransporteUrbanoAPI.Repositories.Interfaces;

public interface IColectivoRepository
{
    List<Colectivo> GetAll(int? codigoLinea, int? codigoEstado);
    Colectivo? GetById(int nroCoche);
    bool ExistePatente(string patente, int? excluirNroCoche);
    bool TieneRegistrosAsociados(int nroCoche);
    Colectivo Add(Colectivo colectivo);
    void Update(Colectivo colectivo);
    void Delete(int nroCoche);
}
