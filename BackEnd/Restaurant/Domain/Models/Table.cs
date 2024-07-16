using Common.Exceptions;

namespace Domain.Models
{
    public class Table
    {
        public Guid Id { get; init; }

        public Guid RestaurantId { get; init; }

        public int Seats { get; private set; }

        public Restaurant? Restaurant { get; init; }

        private Table()
        {
            Id = Guid.Empty;
            RestaurantId = Guid.Empty;
            Seats = 0;
            Restaurant = default;
        }

        private Table(Guid id, Guid restaurantId, int seats)
        {
            Id = id;
            RestaurantId = restaurantId;
            Seats = seats;
            Restaurant = default;
        }

        public static Table Create(Guid restaurantId, int seats)
        {
            if (string.IsNullOrWhiteSpace(seats.ToString()))
            {
                throw new BussinessRuleValidationExeption("Number of seats is required value for table");
            }

            if (seats <= 0 && seats > 12)
            {
                throw new BussinessRuleValidationExeption("Number of seats must be a positive number smaller than 12");
            }

            return new Table(Guid.NewGuid(), restaurantId, seats);
        }
    }
}