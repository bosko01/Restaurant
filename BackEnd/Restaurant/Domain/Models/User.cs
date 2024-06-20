using Common.Exceptions;
using Domain.Enums.User;
using Domain.ValueObjects;

namespace Domain.Models
{
    public class User
    {
        public Guid Id { get; init; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public Email Email { get; init; }

        public string Password { get; private set; }

        public EUserType UserType { get; private set; }

        public PhoneNumber Phone { get; private set; }

        private User()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = Email.Create(string.Empty);
            Password = string.Empty;
            UserType = default;
            Phone = PhoneNumber.Create(string.Empty, string.Empty);
        }

        private User(Guid id, string firstName, string lastName, string email, string password, string countryCode, string phoneNumber)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = Email.Create(email);
            Password = password;
            UserType = EUserType.User;
            Phone = PhoneNumber.Create(countryCode, phoneNumber);
        }

        public static User Create(string firstName, string lastName, string email, string password, string countryCode, string phone)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new BussinessRuleValidationExeption("Name is required value for user");
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new BussinessRuleValidationExeption("Last Name is required value for user");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new BussinessRuleValidationExeption("Password is required");
            }

            return new User(Guid.NewGuid(), firstName, lastName, email, password, countryCode, phone);
        }
    }
}