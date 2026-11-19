using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransporteUrbanoAPI.Dtos;
using TransporteUrbanoAPI.Services.Interfaces;

namespace TransporteUrbanoAPI.Controllers;

[Authorize(Roles = "Admin")]
[Route("api/[controller]")]
[ApiController]
public class MantenimientosController : ApiControllerBase
{
    private readonly IMantenimientoService _service;

    public MantenimientosController(IMantenimientoService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll([FromQuery] int? coche)
    {
        return Ok(_service.GetAll(coche));
    }

    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] int id)
    {
        return Ejecutar(() => Ok(_service.GetById(id)));
    }

    [HttpPost]
    public IActionResult Post([FromBody] MantenimientoSaveDto dto)
    {
        return Ejecutar(() =>
        {
            MantenimientoDto nuevo = _service.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = nuevo.CodigoMantenimiento }, nuevo);
        });
    }

    [HttpPut("{id}")]
    public IActionResult Put([FromRoute] int id, [FromBody] MantenimientoSaveDto dto)
    {
        return Ejecutar(() => { _service.Update(id, dto); return NoContent(); });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        return Ejecutar(() => { _service.Delete(id); return NoContent(); });
    }
}