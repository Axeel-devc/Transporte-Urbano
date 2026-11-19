using Microsoft.AspNetCore.Mvc;
using TransporteUrbanoAPI.Exceptions;

namespace TransporteUrbanoAPI.Controllers;

// Convierte las excepciones de negocio en respuestas HTTP (asi no repetimos try/catch)
public abstract class ApiControllerBase : ControllerBase
{
    protected IActionResult Ejecutar(Func<IActionResult> accion)
    {
        try
        {
            return accion();
        }
        catch (NoEncontradoException e)
        {
            return NotFound(new { message = e.Message });
        }
        catch (ReglaNegocioException e)
        {
            return BadRequest(new { message = e.Message });
        }
    }
}
