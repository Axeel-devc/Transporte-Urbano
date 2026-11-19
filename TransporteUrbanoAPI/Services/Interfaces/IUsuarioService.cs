using TransporteUrbanoAPI.Dtos;
using TransporteUrbanoAPI.Models;

namespace TransporteUrbanoAPI.Services.Interfaces;

public interface IUsuarioService
{
    Usuario Registrar(RegistroDto dto);
}
