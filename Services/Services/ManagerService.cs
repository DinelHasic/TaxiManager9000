using TaxiManager9000.DataAccess.Interface;
using TaxiManager9000.Domain.Entities;
using TaxiManager9000.Domain.Enums;
using TaxiManager9000.Services.Interfaces;
using TaxiManager9000.Shared;

namespace TaxiManager9000.Services.Services
{
    public class ManagerService : IManagerServices
    {
        private readonly IDriverDatabase _driverDatabase;

        private readonly ICarDatabase _carDatabase;

        private DateTime curentDate = DateTime.Now;

        public ManagerService()
        {
            _driverDatabase = DepencyResolver.GetService<IDriverDatabase>();

            _carDatabase = DepencyResolver.GetService<ICarDatabase>();
        }

        public List<Driver> ListOfAllDrivers()
        {
            return _driverDatabase.GetDriversData();
        }

        public List<Driver> ListOfUnasignDrives()
        {
            return _driverDatabase.GetDriversData().Where(x => x.CarDriving == null).ToList();
        }

        public List<Driver> ListOfAssignedDrives()
        {
            return _driverDatabase.GetDriversData().Where(x => x.CarDriving != null).ToList();
        }

        public List<Driver> GetValidLicense()
        {
            return _driverDatabase.GetDriversData().Where(x => x.LicenseExpieryDate > curentDate && x.LicenseExpieryDate.Subtract(curentDate).Days / (365.25 / 12) > 3).ToList();
        }

        public List<Driver> GetExpiredLicense()
        {
            return _driverDatabase.GetDriversData().Where(x => x.LicenseExpieryDate < curentDate).ToList();
        }

        public List<Driver> GetLicenseToExpired()
        {
            return _driverDatabase.GetDriversData().Where(x => x.LicenseExpieryDate.Subtract(curentDate).Days / (365.25 / 12) <= 3 && x.LicenseExpieryDate > curentDate).ToList();
        }

        public async Task AssignCarAndDriverAsync(Shift shift,int carId, int driverId)
        {
            Car car = _carDatabase.GetCarsData().FirstOrDefault(x => x.Id == carId) ?? throw new NullReferenceException("Car not found");

            Driver driver = _driverDatabase.GetDriversData().FirstOrDefault(x => x.Id == driverId) ?? throw new NullReferenceException("Driver not found");

            driver.AssignCar(car);
            driver.AssignShift(shift);


            await _carDatabase.UpdateAsync();
            await _driverDatabase.UpdateAsync();
        }

        public async Task UnasignCarToDriverAsync(int driverId)
        {
            Driver driver = _driverDatabase.GetDriversData().FirstOrDefault(x => x.Id == driverId) ?? throw new NullReferenceException("Driver not found");
            Car car = _carDatabase.GetCarsData().FirstOrDefault(x => x.AssignedDrivers.SingleOrDefault(y => y.Id == driver.Id) != null) ?? throw new NullReferenceException("Car not found");

            
            driver.UnassignCar();
            car.UnAssignDriver(driverId);

            await _driverDatabase.UpdateAsync();
            await _carDatabase.UpdateAsync();
        }
    }
}
