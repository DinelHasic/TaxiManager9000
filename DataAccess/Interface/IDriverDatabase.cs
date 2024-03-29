﻿using TaxiManager9000.Domain.Entities;

namespace TaxiManager9000.DataAccess.Interface
{
    public interface IDriverDatabase : IDatabase<Driver>
    {
        List<Driver> GetDriversData();
    }
}
