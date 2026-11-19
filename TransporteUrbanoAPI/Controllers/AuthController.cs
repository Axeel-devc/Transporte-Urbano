using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TransporteUrbanoAPI.Dtos;
using TransporteUrbanoAPI.Helpers;
using TransporteUrbanoAPI.Models;
using TransporteUrbanoAPI.Repositories.Interfaces;
using TransporteUrbanoAPI.Services.Interfaces;

namespace TransporteUrbanoAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ApiControllerBase
{
    private readonly IUsuarioRepository _repo;
    private readonly IUsuarioService _service;
    private readonly IConfiguration _config;

    public AuthController(IUsuarioRepository repo, IUsuarioService service, IConfiguration config)
    {
        _repo = repo;
        _service = service;
        _config = config;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto dto)
    {
        Usuario? usuario = _repo.Login(dto.NombreUsuario.Trim(), PasswordHelper.Hash(dto.Password));

        if (usuario == null)
            return Unauthorized(new { message = "Usuario o contraseña incorrectos" });

        return Ok(CrearRespuesta(usuario));
    }

    // Registra al usuario y ya lo deja con la sesion iniciada
    [HttpPost("registro")]
    public IActionResult Registro([FromBody] RegistroDto dto)
    {
        return Ejecutar(() => Ok(CrearRespuesta(_service.Registrar(dto))));
    }

    private LoginResponseDto CrearRespuesta(Usuario usuario)
    {
        var jwt = _config.GetSection("JwtSettings");
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, usuario.NombreUsuario),
            new Claim(ClaimTypes.Role, usuario.Rol)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));
        var expira = DateTime.UtcNow.AddMinutes(int.Parse(jwt["ExpiresInMinutes"]!));

        var token = new JwtSecurityToken(
            issuer: jwt["Issuer"],
            audience: jwt["Audience"],
            claims: claims,
            expires: expira,
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

        return new LoginResponseDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Usuario = usuario.NombreUsuario,
            Rol = usuario.Rol,
            Expira = expira
        };
    }
}