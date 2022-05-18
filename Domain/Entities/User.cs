using TaxiManager9000.Domain.Enums;
using TaxiManager9000.Domain.Exceptions;

namespace TaxiManager9000.Domain.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public Role Role { get; set; }

        public User(string userName, string password, Role role)
        {
            if (userName.Length < 5)
            {
                throw new InvalidUserCreatinoExpection("Username has to have at least 5 characters");
            }

            if (!(password.Length >= 5))
            {
                throw new InvalidUserCreatinoExpection("Password has to have at least  5 charaters ");
            }

            if (!(password.Any(char.IsDigit)))
            {
                throw new InvalidUserCreatinoExpection("Password has to have at least one number");
            }

            Id = -1;
            UserName = userName;
            Password = password;
            Role = role;
        }

        public void NewPassword(string newPassword)
        {
           Password = newPassword;
        }
    }
}
