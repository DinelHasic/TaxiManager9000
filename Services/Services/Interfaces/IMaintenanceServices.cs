using TaxiManager9000.Domain.Entities;
using TaxiManager9000.Domain.Enums;

namespace TaxiManager9000.Services.Interfaces
{
    public interface IMaintenanceServices
    {
        List<Car> GetValidLicensePlate();

        List<Car> GetExpiredLicensePlate();

        List<Car> GetLicensePlateToExpired();

        List<Car> GetListOfCars();

        List<Car> GetListAvailableCars(Shift shift);
    }
}
