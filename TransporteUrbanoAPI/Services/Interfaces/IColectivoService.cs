using TransporteUrbanoAPI.Dtos;


namespace TransporteUrbanoAPI.Services.Interfaces;

public interface IColectivoService
{
    List<ColectivoDto> GetAll(int? codigoLinea, int? codigoEstado);
    ColectivoDto GetById(int nroCoche);
    ColectivoDto Create(ColectivoSaveDto dto);
    void Update(int nroCoche, ColectivoSaveDto dto);
    void Delete(int nroCoche);
}
