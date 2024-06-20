using Common.Exceptions;
using Domain.Enums.Reservation;

namespace Domain.Models
{
    public class Reservation
    {
        public Guid Id { get; init; }

        public Guid UserId { get; init; }

        public User? User { get; set; }

        public Guid RestaurantId { get; init; }

        public Restaurant? Restaurant { get; set; }

        public Guid TableId { get; init; }

        public Table? Table { get; set; }

        public int NumberOfPeople { get; init; }

        public decimal Price { get; init; }

        public EStatusReservation Status { get; private set; }

        private Reservation(Guid id, Guid userId, Guid restaurantId, Guid tableId, int numOfPeople)
        {
            Id = id;
            UserId = userId;
            RestaurantId = restaurantId;
            TableId = tableId;
            NumberOfPeople = numOfPeople;
            Price = numOfPeople * 50;
            Status = EStatusReservation.Active;
        }

        private Reservation()
        {
            Id = Guid.Empty;
            UserId = Guid.Empty;
            RestaurantId = Guid.Empty;
            TableId = Guid.Empty;
            NumberOfPeople = 0;
            Price = 0;
            Status = default;
        }

        public static Reservation Create(Guid userId, Guid restaurantId, Guid tableId, int numOfPeople)
        {
            if (string.IsNullOrWhiteSpace(numOfPeople.ToString()))
            {
                throw new BussinessRuleValidationExeption("Number of people is required value for reservation");
            }

            return new Reservation(Guid.NewGuid(), userId, restaurantId, tableId, numOfPeople);
        }
    }
}