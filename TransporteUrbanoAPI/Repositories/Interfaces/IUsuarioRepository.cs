using TransporteUrbanoAPI.Models;

namespace TransporteUrbanoAPI.Repositories.Interfaces;

public interface IUsuarioRepository
{
    Usuario? Login(string usuarioOEmail, string passwordHash);
    bool ExisteNombreUsuario(string nombreUsuario);
    bool ExisteEmail(string email);
    Usuario Add(Usuario usuario);
}