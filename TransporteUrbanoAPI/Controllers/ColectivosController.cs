using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransporteUrbanoAPI.Dtos;
using TransporteUrbanoAPI.Services.Interfaces;

namespace TransporteUrbanoAPI.Controllers;

[Authorize(Roles = "Admin")]
[Route("api/[controller]")]
[ApiController]
public class ColectivosController : ApiControllerBase
{
    private readonly IColectivoService _service;

    public ColectivosController(IColectivoService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll([FromQuery] int? linea, [FromQuery] int? estado)
    {
        return Ok(_service.GetAll(linea, estado));
    }

    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] int id)
    {
        return Ejecutar(() => Ok(_service.GetById(id)));
    }

    [HttpPost]
    public IActionResult Post([FromBody] ColectivoSaveDto dto)
    {
        return Ejecutar(() =>
        {
            ColectivoDto nuevo = _service.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = nuevo.NroCoche }, nuevo);
        });
    }

    [HttpPut("{id}")]
    public IActionResult Put([FromRoute] int id, [FromBody] ColectivoSaveDto dto)
    {
        return Ejecutar(() => { _service.Update(id, dto); return NoContent(); });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        return Ejecutar(() => { _service.Delete(id); return NoContent(); });
    }
}