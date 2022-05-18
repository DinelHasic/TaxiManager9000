using TaxiManager9000.DataAccess.Interface;
using TaxiManager9000.Domain.Entities;
using TaxiManager9000.Domain.Enums;
using TaxiManager9000.Services.Interfaces;
using TaxiManager9000.Shared;

namespace TaxiManager9000.Services
{
    public class MaintenanceServices : IMaintenanceServices
    {
        private readonly ICarDatabase _database;

        private readonly DateTime curentDate = DateTime.Now;

        public MaintenanceServices()
        {
            _database = DepencyResolver.GetService<ICarDatabase>();
        }

        public List<Car> GetValidLicensePlate()
        {
            List<Car> cars = _database.GetCarsData();

            return cars.Where(x => x.LicensePlateExpieryDate > curentDate && x.LicensePlateExpieryDate.Subtract(curentDate).Days / (365.25 / 12) > 3).ToList();
        }

        public List<Car> GetExpiredLicensePlate()
        {
            List<Car> cars = _database.GetCarsData();

            return cars.Where(x => x.LicensePlateExpieryDate < curentDate).ToList();
        }

        public List<Car> GetLicensePlateToExpired()
        {
            List<Car> cars = _database.GetCarsData();

            return cars.Where(x => x.LicensePlateExpieryDate.Subtract(curentDate).Days / (365.25 / 12) <= 3 && x.LicensePlateExpieryDate > curentDate).ToList();
        }

        public List<Car> GetListOfCars()
        {
            return _database.GetCarsData();
        }

        public List<Car> GetListAvailableCars(Shift shift)
        {
            return _database.GetCarsData().Where(x => x.LicensePlateExpieryDate > curentDate && x.AssignedDrivers.Count <= 0 || x.AssignedDrivers.Any(x => x.Shift != shift)).ToList();
        }
    }
}

