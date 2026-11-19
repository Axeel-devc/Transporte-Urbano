using TransporteUrbanoAPI.Dtos;


namespace TransporteUrbanoAPI.Services.Interfaces;

public interface IMantenimientoService
{
    List<MantenimientoDto> GetAll(int? nroCoche);
    MantenimientoDto GetById(int codigoMantenimiento);
    MantenimientoDto Create(MantenimientoSaveDto dto);
    void Update(int codigoMantenimiento, MantenimientoSaveDto dto);
    void Delete(int codigoMantenimiento);
}
