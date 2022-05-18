using Newtonsoft.Json;

namespace TaxiManager9000.Domain.Entities
{
    public class Car : BaseEntity
    {
        public string Model { get; set; }

        public string LicensePlate { get; set; }

        public DateTime LicensePlateExpieryDate { get; set; }

        public List<Driver> AssignedDrivers { get; set; }

        public Car(string model, string licensePlate, DateTime licensePlateExpieryDate)
        {
            Id = -1;
            Model = model;
            LicensePlate = licensePlate;
            LicensePlateExpieryDate = licensePlateExpieryDate;
            AssignedDrivers = new List<Driver>();
        }

        [JsonConstructor]
        public Car(string model, string licensePlate, DateTime licensePlateExpieryDate, List<Driver> asignedDrivers)
        {
            Id = -1;
            Model = model;
            LicensePlate = licensePlate;
            LicensePlateExpieryDate = licensePlateExpieryDate;
            AssignedDrivers = asignedDrivers;
        }

        public override string ToString()
        {
            int assignedPercent = AssignedDrivers.Count == 0 ? 0 : 100 / 3 * AssignedDrivers.Count + 1;
            return $"Id:{Id} Model:{Model} LiscancePlate:{LicensePlate} and utilized {assignedPercent}%";
        }

        public void AssignDriver(Driver driver)
        {
            AssignedDrivers.Add(driver);
        }

        public void UnAssignDriver(int driverId)
        {
          Driver driver = AssignedDrivers.SingleOrDefault(x => x.Id == driverId);

         AssignedDrivers.Remove(driver);
        }
    }
}
