using TaxiManager9000.DataAccess.Interface;
using TaxiManager9000.Domain.Entities;

namespace TaxiManager9000.DataAccess
{
    public class UserDatabase : FileDatabase<User>, IUserDatabase
    {
        public UserDatabase()
        {
           /* Seed();
            UpdateAsync();*/
        }

        public User GetByUserNameAndPassword(string username, string password)
        {
            return Items.FirstOrDefault(user => user.UserName == username &&
                                                user.Password == password);
        }

        // DB
        private void Seed()
        {
            Items.Add(AutoIncrementId(new User("test1", "test11", Domain.Enums.Role.Administrator)));
            Items.Add(AutoIncrementId(new User("test2", "test22", Domain.Enums.Role.Manager)));
            Items.Add(AutoIncrementId(new User("test3", "test33", Domain.Enums.Role.Maintainance)));
            Items.Add(AutoIncrementId(new User("test4", "test44", Domain.Enums.Role.Administrator)));
        }

        public List<string> GetUserNames()
        {
            return Items.Where(_users => _users.Role != Domain.Enums.Role.Administrator).Select(_users => _users.UserName).ToList();
        }

        public void RemoveUserByUsername(string username)
        {
            User user = Items.FirstOrDefault(x => x.UserName == username);

            Items.Remove(user);
        }
    }
}
