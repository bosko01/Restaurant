using Common.Exceptions;
using Domain.Enums.User;
using Domain.ValueObjects;

namespace Domain.Models
{
    public class User
    {
        public Guid Id { get; init; }

        public Guid CredentialsId { get; init; }

        public UserCredentials? Credentials { get; init; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public EUserType UserType { get; init; }

        public PhoneNumber Phone { get; private set; }

        private User()
        {
            CredentialsId = Guid.Empty;
            Credentials = default!;
            FirstName = string.Empty;
            LastName = string.Empty;
            UserType = default;
            Phone = default!;
        }

        private User(Guid id, Guid credentialsId, string firstName, string lastName, EUserType userType, PhoneNumber phone)
        {
            Id = id;
            CredentialsId = credentialsId;
            FirstName = firstName;
            LastName = lastName;
            UserType = userType;
            Phone = phone;
        }

        public static User Create(string credentialsID, string firstName, string lastName,string userType, string countryCode, string phone)
        {
            if (string.IsNullOrWhiteSpace(credentialsID))
            {
                throw new BussinessRuleValidationExeption("CredentialsId is required value for user");
            }

            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new BussinessRuleValidationExeption("Name is required value for user");
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new BussinessRuleValidationExeption("Last Name is required value for user");
            }

            if (string.IsNullOrWhiteSpace(userType))
            {
                throw new BussinessRuleValidationExeption("Last Name is required value for user");
            }

            PhoneNumber userPhone = PhoneNumber.Create(countryCode, phone);

            return new User(Guid.NewGuid(), new Guid(credentialsID), firstName, lastName,Enum.Parse<EUserType>(userType), userPhone);
        }

        public void Update(string firstName, string lastName, string countryCode, string phone)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new BussinessRuleValidationExeption("Name is required value for user");
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new BussinessRuleValidationExeption("Last Name is required value for user");
            }

            FirstName = firstName;
            LastName = lastName;
            Phone = PhoneNumber.Create(countryCode, phone);
        }
    }
}