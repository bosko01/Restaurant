using Common.Exceptions;
using Domain.Enums.Restaurant;
using Domain.ValueObjects;

namespace Domain.Models
{
    public class Restaurant
    {
        public Guid Id { get; init; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public string Location { get; private set; }

        public Email Email { get; private set; }

        public PhoneNumber Phone { get; private set; }

        public string Menu { get; private set; }

        public List<ERestaurantCategories> Category { get; private set; }

        public List<Review> Reviews { get; private set; }

        public List<Table> Tables { get; private set; }

        private Restaurant(Guid id, string name, string description, string location, string email, string countryCode, string phoneNumber, string menu)
        {
            Id = id;
            Name = name;
            Description = description;
            Location = location;
            Email = Email.Create(email);
            Phone = PhoneNumber.Create(countryCode, phoneNumber);
            Menu = menu;
            Category = new List<ERestaurantCategories>();
            Reviews = new List<Review>();
            Tables = new List<Table>();
        }

        public static Restaurant Create(string name, string description, string location, string email, string countryCode, string phone, string menu)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new BussinessRuleValidationExeption("Name is required value for restaurant");
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                throw new BussinessRuleValidationExeption("Description is required for restaurant");
            }

            if (string.IsNullOrWhiteSpace(location))
            {
                throw new BussinessRuleValidationExeption("Location is required for restaurant");
            }

            if (string.IsNullOrWhiteSpace(menu))
            {
                throw new BussinessRuleValidationExeption("Menu is required for restaurant");
            }

            return new Restaurant(Guid.NewGuid(), name, description, location, email, countryCode, phone, menu);
        }
    }
}