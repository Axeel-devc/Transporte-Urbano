using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransporteUrbanoAPI.Repositories.Interfaces;

namespace TransporteUrbanoAPI.Controllers;

// Datos de solo lectura para los combos. Sin reglas de negocio, por eso van directo al repositorio.
[Authorize(Roles = "Admin")]
[Route("api")]
[ApiController]
public class CatalogosController : ControllerBase
{
    private readonly ICatalogoRepository _repo;

    public CatalogosController(ICatalogoRepository repo)
    {
        _repo = repo;
    }

    [HttpGet("lineas")]
    public IActionResult GetLineas() => Ok(_repo.GetLineas());

    [HttpGet("estados-colectivo")]
    public IActionResult GetEstados() => Ok(_repo.GetEstados());

    [HttpGet("tipos-mantenimiento")]
    public IActionResult GetTiposMantenimiento() => Ok(_repo.GetTiposMantenimiento());
}
