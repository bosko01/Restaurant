using Common.Exceptions;
using Domain.Enums.Reservation;

namespace Domain.Models
{
    public class Reservation
    {
        public Guid Id { get; init; }

        public Guid UserId { get; init; }

        public Guid RestaurantId { get; init; }

        public Guid TableId { get; init; }

        public int NumberOfPeople { get; init; }

        public decimal Price { get; init; }

        public EStatusReservation Status { get; private set; }

        private Reservation(Guid id, Guid userId, Guid restaurantId, Guid table, int numOfPeople, decimal price)
        {
            Id = id;
            UserId = userId;
            RestaurantId = restaurantId;
            TableId = table;
            NumberOfPeople = numOfPeople;
            Price = price;
            Status = EStatusReservation.Active;
        }

        public static Reservation Create(User user, Restaurant restaurant, Table table, int numOfPeople)
        {
            if (string.IsNullOrWhiteSpace(numOfPeople.ToString()))
            {
                throw new BussinessRuleValidationExeption("Number of people is required value for reservation");
            }

            if (numOfPeople > table.Seats)
            {
                throw new BussinessRuleValidationExeption("Number of people expected for this reservation is greater than number of seats on given table");
            }

            return new Reservation(Guid.NewGuid(), user.Id, restaurant.Id, table.Id, numOfPeople, table.DepositAmount);
        }
    }
}