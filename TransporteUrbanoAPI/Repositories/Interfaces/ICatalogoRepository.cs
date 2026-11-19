using TransporteUrbanoAPI.Dtos;

namespace TransporteUrbanoAPI.Repositories.Interfaces;

public interface ICatalogoRepository
{
    List<ItemDto> GetLineas();
    List<ItemDto> GetEstados();
    List<ItemDto> GetTiposMantenimiento();
    bool ExisteLinea(int id);
    bool ExisteEstado(int id);
    bool ExisteTipoMantenimiento(int id);
}
