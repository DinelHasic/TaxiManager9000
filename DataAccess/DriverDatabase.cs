using TaxiManager9000.DataAccess.Interface;
using TaxiManager9000.Domain.Entities;

namespace TaxiManager9000.DataAccess
{
    public class DriverDatabase : FileDatabase<Driver>, IDriverDatabase
    {
        public DriverDatabase()
        {
            /*Seed();
            UpdateAsync();*/
        }

        // DB
      /*  private void Seed()
        {
            Items.Add(AutoIncrementId(new Driver("Driver1", "Driver11", "111-2222-3333-444", new DateTime(2022, 9, 1))));
            Items.Add(AutoIncrementId(new Driver("Driver2", "Driver22", "999-666-777-888", new DateTime(2024, 1, 1))));
            Items.Add(AutoIncrementId(new Driver("Driver3", "Driver33", "222-333-444-555", new DateTime(2022, 10, 2))));
            Items.Add(AutoIncrementId(new Driver("Driver4", "Driver44", "s33-333-444-555", new DateTime(2022, 5, 14))));
            Items.Add(AutoIncrementId(new Driver("Driver5", "Driver55", "333-333-333-3335", new DateTime(2023, 8, 1))));
            Items.Add(AutoIncrementId(new Driver("Driver6", "Driver66", "133-113-411-515", new DateTime(2022, 7, 4))));
            Items.Add(AutoIncrementId(new Driver("Driver7", "Driver77", "132-221-434-858", new DateTime(2022, 4, 1))));
        }
        */

        public List<Driver> GetDriversData()
        {
            return Items;
        }
    }
}
