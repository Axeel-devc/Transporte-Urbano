using TransporteUrbanoAPI.Dtos;
using TransporteUrbanoAPI.Exceptions;
using TransporteUrbanoAPI.Models;
using TransporteUrbanoAPI.Repositories.Interfaces;
using TransporteUrbanoAPI.Services.Interfaces;

namespace TransporteUrbanoAPI.Services;

public class MantenimientoService : IMantenimientoService
{
    private readonly IMantenimientoRepository _repo;
    private readonly IColectivoRepository _colectivos;
    private readonly ICatalogoRepository _catalogo;

    public MantenimientoService(IMantenimientoRepository repo, IColectivoRepository colectivos, ICatalogoRepository catalogo)
    {
        _repo = repo;
        _colectivos = colectivos;
        _catalogo = catalogo;
    }

    public List<MantenimientoDto> GetAll(int? nroCoche)
    {
        return _repo.GetAll(nroCoche).Select(ToDto).ToList();
    }

    public MantenimientoDto GetById(int codigoMantenimiento)
    {
        Mantenimiento m = _repo.GetById(codigoMantenimiento)
            ?? throw new NoEncontradoException("El mantenimiento no existe");
        return ToDto(m);
    }

    public MantenimientoDto Create(MantenimientoSaveDto dto)
    {
        Validar(dto);

        Mantenimiento nuevo = new Mantenimiento
        {
            NroCoche = dto.NroCoche,
            CodigoTipoMantenimiento = dto.CodigoTipoMantenimiento,
            Fecha = dto.Fecha,
            Descripcion = string.IsNullOrWhiteSpace(dto.Descripcion) ? null : dto.Descripcion.Trim(),
            Costo = dto.Costo
        };
        _repo.Add(nuevo);

        return ToDto(_repo.GetById(nuevo.CodigoMantenimiento)!);
    }

    public void Update(int codigoMantenimiento, MantenimientoSaveDto dto)
    {
        Mantenimiento m = _repo.GetById(codigoMantenimiento)
            ?? throw new NoEncontradoException("El mantenimiento no existe");

        Validar(dto);

        m.NroCoche = dto.NroCoche;
        m.CodigoTipoMantenimiento = dto.CodigoTipoMantenimiento;
        m.Fecha = dto.Fecha;
        m.Descripcion = string.IsNullOrWhiteSpace(dto.Descripcion) ? null : dto.Descripcion.Trim();
        m.Costo = dto.Costo;

        _repo.Update(m);
    }

    public void Delete(int codigoMantenimiento)
    {
        if (_repo.GetById(codigoMantenimiento) == null)
            throw new NoEncontradoException("El mantenimiento no existe");

        _repo.Delete(codigoMantenimiento);
    }

    // ----- reglas de negocio -----
    private void Validar(MantenimientoSaveDto dto)
    {
        if (dto.Fecha == default)
            throw new ReglaNegocioException("La fecha es obligatoria");
        if (dto.Fecha > DateOnly.FromDateTime(DateTime.Today))
            throw new ReglaNegocioException("La fecha del mantenimiento no puede ser futura");
        if (dto.Costo is < 0)
            throw new ReglaNegocioException("El costo no puede ser negativo");
        if (_colectivos.GetById(dto.NroCoche) == null)
            throw new ReglaNegocioException("El colectivo indicado no existe");
        if (!_catalogo.ExisteTipoMantenimiento(dto.CodigoTipoMantenimiento))
            throw new ReglaNegocioException("El tipo de mantenimiento indicado no existe");
    }

    private static MantenimientoDto ToDto(Mantenimiento m)
    {
        return new MantenimientoDto
        {
            CodigoMantenimiento = m.CodigoMantenimiento,
            NroCoche = m.NroCoche,
            Patente = m.NroCocheNavigation.Patente,
            CodigoTipoMantenimiento = m.CodigoTipoMantenimiento,
            TipoMantenimiento = m.CodigoTipoMantenimientoNavigation.Descripcion,
            Fecha = m.Fecha,
            Descripcion = m.Descripcion,
            Costo = m.Costo
        };
    }
}
