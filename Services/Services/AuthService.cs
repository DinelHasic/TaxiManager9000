using TaxiManager9000.DataAccess.Interface;
using TaxiManager9000.Domain.Entities;
using TaxiManager9000.Domain.Exceptions;
using TaxiManager9000.Services.Interfaces;
using TaxiManager9000.Shared;

namespace TaxiManager9000.Services
{
    public class AuthService : IAuthService
    {
        public User CurrentUser { get; private set; }

        private readonly IUserDatabase _database;

        public AuthService()
        {
            _database = DepencyResolver.GetService<IUserDatabase>();
        }

        public void LogIn(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException();
            }

            User currentUser = _database.GetByUserNameAndPassword(username, password);

            CurrentUser = currentUser ?? throw new ArgumentNullException();
        }

        public async Task SetNewPasswordAsync(string oldPassword, string newPassword)
        {
            if (string.IsNullOrEmpty(oldPassword))
            {
                throw new InvalidPasswordCreationExpection("Old password is empty");
            }

            if (string.IsNullOrEmpty(newPassword))
            {
                throw new InvalidPasswordCreationExpection("New password is empty");
            }

            bool containsNum = newPassword.Any(char.IsDigit);

            if (!(containsNum))
            {
                throw new InvalidPasswordCreationExpection("Password has to contain a number");
            }

            User user = _database.GetByUserNameAndPassword(CurrentUser.UserName, oldPassword) ?? throw new InvalidPasswordCreationExpection("Password unssecsesful changed!");


            user.NewPassword(newPassword);

            await _database.UpdateAsync();

            CurrentUser = user;
        }
    }
}
