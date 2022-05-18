using TaxiManager9000.Domain.Entities;

namespace TaxiManager9000.DataAccess.Interface
{
    public interface IUserDatabase : IDatabase<User>
    {
        Task InsertAsync(User user);

        User GetByUserNameAndPassword(string username, string password);

        List<string> GetUserNames();

        void RemoveUserByUsername(string username);
    }
}
