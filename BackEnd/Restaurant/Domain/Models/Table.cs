using Common.Exceptions;

namespace Domain.Models
{
    public class Table
    {
        public Guid Id { get; init; }

        public Guid RestaurantId { get; init; }

        public int Seats { get; private set; }

        public decimal DepositAmount { get; private set; }

        private Table(Guid id, Guid restaurantId, int seats, decimal depositAmount)
        {
            Id = id;
            RestaurantId = restaurantId;
            Seats = seats;
            DepositAmount = depositAmount;
        }

        public static Table Create(Restaurant restaurant, int seats, decimal depositAmount)
        {
            if (string.IsNullOrWhiteSpace(seats.ToString()))
            {
                throw new BussinessRuleValidationExeption("Number of seats is required value for table");
            }

            if (seats <= 0 && seats > 12)
            {
                throw new BussinessRuleValidationExeption("Number of seats must be a positive number smaller than 12");
            }

            if (string.IsNullOrWhiteSpace(depositAmount.ToString()))
            {
                throw new BussinessRuleValidationExeption("Deposit amount is a required number for table");
            }

            if (depositAmount < 0)
            {
                throw new BussinessRuleValidationExeption("Deposit cant be value smaller than 0");
            }

            return new Table(Guid.NewGuid(), restaurant.Id, seats, depositAmount);
        }
    }
}