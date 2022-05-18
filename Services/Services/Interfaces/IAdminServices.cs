using TaxiManager9000.Domain.Entities;
using TaxiManager9000.Domain.Enums;

namespace TaxiManager9000.Services.Interfaces
{
    public interface IAdminServices
    {
        Task CreateUserAsync(string usernam, string password, Role role);

        Task RemoveUserAsync(string username);

        public List<string> ListOfUserNames();
    }
}
