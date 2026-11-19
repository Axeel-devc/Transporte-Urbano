using TransporteUrbanoAPI.Models;
using TransporteUrbanoAPI.Repositories.Interfaces;

namespace TransporteUrbanoAPI.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly TransporteUrbanoContext _context;

    public UsuarioRepository(TransporteUrbanoContext context)
    {
        _context = context;
    }

    // Se puede ingresar con el nombre de usuario o con el email
    public Usuario? Login(string usuarioOEmail, string passwordHash)
    {
        Usuario? usuario = _context.Usuarios.FirstOrDefault(u =>
            (u.NombreUsuario == usuarioOEmail || u.Email == usuarioOEmail) && u.PasswordHash == passwordHash);

        if (usuario != null)
        {
            usuario.UltimoAcceso = DateTime.Now;
            _context.SaveChanges();
        }
        return usuario;
    }

    public bool ExisteNombreUsuario(string nombreUsuario) => _context.Usuarios.Any(u => u.NombreUsuario == nombreUsuario);

    public bool ExisteEmail(string email) => _context.Usuarios.Any(u => u.Email == email);

    public Usuario Add(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        _context.SaveChanges();
        return usuario;
    }
}