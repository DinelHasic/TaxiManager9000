using TaxiManager9000.DataAccess.Interface;
using TaxiManager9000.Domain.Entities;

namespace TaxiManager9000.DataAccess
{
    public class CarDatabase : FileDatabase<Car>, ICarDatabase
    {
        public CarDatabase()
        {
           /* if (Items == null)
            {
                Seed();
                UpdateAsync();
            }
*/
        /*    Seed();
            UpdateAsync();*/
        }
        public List<Car> GetCarsData()
        {
            return Items;
        }

        // DB
        private void Seed()
        {
            Items.Add(AutoIncrementId(new Car("Golf", "MK-3344-SK", new DateTime(2022, 7, 2))));
            Items.Add(AutoIncrementId(new Car("A3", "MK-1314-SK", new DateTime(2022, 1, 29))));
            Items.Add(AutoIncrementId(new Car("A4", "MK-5555-SK", new DateTime(2022, 12, 1))));
            Items.Add(AutoIncrementId(new Car("A6", "MK-2949-SK", new DateTime(2023, 2, 14))));
            Items.Add(AutoIncrementId(new Car("Golf", "MK-2222-SK", new DateTime(2022, 2, 15))));
            Items.Add(AutoIncrementId(new Car("A3", "MK-1177-SK", new DateTime(2023, 3, 20))));
            Items.Add(AutoIncrementId(new Car("A6", "MK-5556-SK", new DateTime(2022, 5, 30))));
        }
    }
}

