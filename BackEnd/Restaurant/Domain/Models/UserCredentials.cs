using Common.Exceptions;
using Domain.ValueObjects;

namespace Domain.Models
{
    public class UserCredentials
    {
        public Guid Id { get; init; }

        public Email Email { get; init; }

        public string Password { get; private set; }

        public string Salt { get; private set; }

        private UserCredentials(Guid id, Email email, string password, string salt)
        {
            Id = id;
            Email = email;
            Password = password;
            Salt = salt;
        }

        public static UserCredentials Create(string email, string password, string salt)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new BussinessRuleValidationExeption("Invalid email");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new BussinessRuleValidationExeption("Invalid password");
            }

            if (string.IsNullOrWhiteSpace(salt))
            {
                throw new BussinessRuleValidationExeption("Invalid salt");
            }

            return new UserCredentials(Guid.NewGuid(), Email.Create(email), password, salt);
        }

        public void Update(string password, string salt)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new BussinessRuleValidationExeption("Invalid password");
            }

            if (string.IsNullOrWhiteSpace(salt))
            {
                throw new BussinessRuleValidationExeption("Invalid salt");
            }

            Password = password;
            Salt = salt;
        }
    }
}