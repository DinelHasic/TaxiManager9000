using TaxiManager9000.Domain.Entities;
using TaxiManager9000.Domain.Enums;

namespace TaxiManager9000.Services.Interfaces
{
    public interface IManagerServices
    {
        List<Driver> ListOfAllDrivers();

        List<Driver> ListOfUnasignDrives();

        List<Driver> ListOfAssignedDrives();

        List<Driver> GetValidLicense();

        List<Driver> GetExpiredLicense();

        List<Driver> GetLicenseToExpired();

        Task AssignCarAndDriverAsync(Shift shift,int carId, int driverId);

        Task UnasignCarToDriverAsync(int driverId);
    }
}
