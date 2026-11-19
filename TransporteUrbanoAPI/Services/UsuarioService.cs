using TransporteUrbanoAPI.Dtos;
using TransporteUrbanoAPI.Exceptions;
using TransporteUrbanoAPI.Helpers;
using TransporteUrbanoAPI.Models;
using TransporteUrbanoAPI.Repositories.Interfaces;
using TransporteUrbanoAPI.Services.Interfaces;

namespace TransporteUrbanoAPI.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _repo;

    public UsuarioService(IUsuarioRepository repo)
    {
        _repo = repo;
    }

    public Usuario Registrar(RegistroDto dto)
    {
        string nombre = dto.NombreUsuario.Trim();
        string email = dto.Email.Trim().ToLower();

        if (_repo.ExisteNombreUsuario(nombre))
            throw new ReglaNegocioException("Ese nombre de usuario ya está en uso");
        if (_repo.ExisteEmail(email))
            throw new ReglaNegocioException("Ese email ya está registrado");

        Usuario nuevo = new Usuario
        {
            NombreUsuario = nombre,
            Email = email,
            PasswordHash = PasswordHelper.Hash(dto.Password),
            Rol = "Visitante"          // el registro NUNCA crea administradores
        };

        return _repo.Add(nuevo);
    }
}
