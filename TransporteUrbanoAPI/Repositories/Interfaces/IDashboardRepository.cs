using TransporteUrbanoAPI.Dtos;

namespace TransporteUrbanoAPI.Repositories.Interfaces;

public interface IDashboardRepository
{
    DashboardDto GetResumen();
}
