using TaxiManager9000.DataAccess.Interface;
using TaxiManager9000.Domain.Entities;
using TaxiManager9000.Domain.Enums;
using TaxiManager9000.Domain.Exceptions;
using TaxiManager9000.Services.Interfaces;
using TaxiManager9000.Shared;

namespace TaxiManager9000.Services
{
    public class AdminServices : IAdminServices
    {
        private readonly IUserDatabase _database;

        public AdminServices()
        {
            _database = DepencyResolver.GetService<IUserDatabase>();
        }

        public async Task CreateUserAsync(string username, string password, Role role)
        {
            bool isUsername = _database.GetUserNames().Any(x => x == username);

            if (isUsername)
            {
                throw new InvalidUserCreatinoExpection("Usernam already exists");
            }

            User user = new User(username, password, role);

            await _database.InsertAsync(user);
        }

        public List<string> ListOfUserNames()
        {
            return _database.GetUserNames();
        }

        public async Task RemoveUserAsync(string username)
        {
            List<string> users = _database.GetUserNames();

            if (!(users.Contains(username)))
            {
                throw new InvalidUserRemoveExpextion("That name dosen't contain in the list");
            }

            _database.RemoveUserByUsername(username);

            await _database.UpdateAsync();
        }
    }
}
