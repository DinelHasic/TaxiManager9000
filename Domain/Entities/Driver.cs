using Newtonsoft.Json;
using TaxiManager9000.Domain.Enums;

namespace TaxiManager9000.Domain.Entities
{
    public class Driver : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }


        public Shift? Shift { get; set; }

        public Car CarDriving { get; set; }

        public string License { get; set; }

        public DateTime LicenseExpieryDate { get; set; }

        public Driver(string firstName, string lastName, string license, DateTime licenseExpieryDate)
        {
            Id = -1;
            FirstName = firstName;
            LastName = lastName;
            License = license;
            LicenseExpieryDate = licenseExpieryDate;
        }

        [JsonConstructor]
        public Driver(string firstName, string lastName, string license, DateTime licenseExpieryDate, Car car)
        {
            Id = -1;
            FirstName = firstName;
            LastName = lastName;
            License = license;
            LicenseExpieryDate = licenseExpieryDate;
            CarDriving = car;
        }

        public override string ToString()
        {
            return $"Id:{Id} FullName:{FirstName} {LastName} Shift:{Shift} Car:{CarDriving?.Model ?? "Unassigned"}";
        }

        public void AssignCar(Car car)
        {
            CarDriving = car;
            car.AssignedDrivers.Add(this);
        }

        public void UnassignCar()
        {
            CarDriving.AssignedDrivers.Remove(this);
            CarDriving = null;
            Shift = null;
        }

        public void AssignShift(Shift shift)
        {
            Shift = shift;
        }
    }
}