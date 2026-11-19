using TransporteUrbanoAPI.Dtos;
using TransporteUrbanoAPI.Exceptions;
using TransporteUrbanoAPI.Models;
using TransporteUrbanoAPI.Repositories.Interfaces;
using TransporteUrbanoAPI.Services.Interfaces;

namespace TransporteUrbanoAPI.Services;

public class ColectivoService : IColectivoService
{
    private readonly IColectivoRepository _repo;
    private readonly ICatalogoRepository _catalogo;

    public ColectivoService(IColectivoRepository repo, ICatalogoRepository catalogo)
    {
        _repo = repo;
        _catalogo = catalogo;
    }

    public List<ColectivoDto> GetAll(int? codigoLinea, int? codigoEstado)
    {
        return _repo.GetAll(codigoLinea, codigoEstado).Select(ToDto).ToList();
    }

    public ColectivoDto GetById(int nroCoche)
    {
        Colectivo colectivo = _repo.GetById(nroCoche)
            ?? throw new NoEncontradoException("El colectivo no existe");
        return ToDto(colectivo);
    }

    public ColectivoDto Create(ColectivoSaveDto dto)
    {
        string patente = Validar(dto, null);

        Colectivo nuevo = new Colectivo
        {
            Patente = patente,
            Marca = Limpiar(dto.Marca),
            Modelo = Limpiar(dto.Modelo),
            CodigoLinea = dto.CodigoLinea,
            CodigoEstado = dto.CodigoEstado
        };
        _repo.Add(nuevo);

        return ToDto(_repo.GetById(nuevo.NroCoche)!);
    }

    public void Update(int nroCoche, ColectivoSaveDto dto)
    {
        Colectivo colectivo = _repo.GetById(nroCoche)
            ?? throw new NoEncontradoException("El colectivo no existe");

        colectivo.Patente = Validar(dto, nroCoche);
        colectivo.Marca = Limpiar(dto.Marca);
        colectivo.Modelo = Limpiar(dto.Modelo);
        colectivo.CodigoLinea = dto.CodigoLinea;
        colectivo.CodigoEstado = dto.CodigoEstado;

        _repo.Update(colectivo);
    }

    public void Delete(int nroCoche)
    {
        if (_repo.GetById(nroCoche) == null)
            throw new NoEncontradoException("El colectivo no existe");

        if (_repo.TieneRegistrosAsociados(nroCoche))
            throw new ReglaNegocioException(
                "No se puede eliminar: el colectivo tiene mantenimientos, servicios o asignaciones registrados. " +
                "Cambiá su estado a \"Fuera de servicio\".");

        _repo.Delete(nroCoche);
    }

    // ----- reglas de negocio -----
    private string Validar(ColectivoSaveDto dto, int? nroCoche)
    {
        string patente = dto.Patente.Trim().ToUpper();

        if (_repo.ExistePatente(patente, nroCoche))
            throw new ReglaNegocioException("Ya existe un colectivo con esa patente");
        if (!_catalogo.ExisteLinea(dto.CodigoLinea))
            throw new ReglaNegocioException("La línea indicada no existe");
        if (!_catalogo.ExisteEstado(dto.CodigoEstado))
            throw new ReglaNegocioException("El estado indicado no existe");

        return patente;
    }

    private static string? Limpiar(string? texto)
    {
        return string.IsNullOrWhiteSpace(texto) ? null : texto.Trim();
    }

    private static ColectivoDto ToDto(Colectivo c)
    {
        return new ColectivoDto
        {
            NroCoche = c.NroCoche,
            Patente = c.Patente,
            Marca = c.Marca,
            Modelo = c.Modelo,
            CodigoLinea = c.CodigoLinea,
            NumeroLinea = c.CodigoLineaNavigation.NumeroLinea,
            CodigoEstado = c.CodigoEstado,
            Estado = c.CodigoEstadoNavigation.Descripcion
        };
    }
}