using Common.Exceptions;
using Domain.Enums.Reservation;

namespace Domain.Models
{
    public class Reservation
    {
        public Guid Id { get; init; }

        public Guid UserId { get; init; }

        public User? User { get; private set; }

        public Guid RestaurantId { get; init; }

        public Restaurant? Restaurant { get; private set; }

        public Guid TableId { get; init; }

        public Table? Table { get; private set; }

        public int NumberOfPeople { get; init; }

        public decimal Price { get; init; }

        public TimeOnly DurationFrom { get; init; }

        public TimeOnly DurationTo { get; init; }

        public EStatusReservation Status { get; private set; }

        private Reservation(Guid id, Guid userId, Guid restaurantId, Guid tableId, int numOfPeople, EStatusReservation status, TimeOnly durationFrom, TimeOnly durationTo)
        {
            Id = id;
            UserId = userId;
            RestaurantId = restaurantId;
            TableId = tableId;
            NumberOfPeople = numOfPeople;
            Price = numOfPeople * 50;
            Status = status;
            DurationFrom = durationFrom;
            DurationTo = durationTo;
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
            DurationFrom = new TimeOnly(0,50,0);
            DurationTo = new TimeOnly(0, 51, 0);
        }

        public static Reservation Create(Guid userId, Guid restaurantId, Guid tableId, int numOfPeople, TimeOnly durationFrom, TimeOnly durationTo)
        {
            if (string.IsNullOrWhiteSpace(userId.ToString()))
            {
                throw new BussinessRuleValidationExeption("User is required value for reservation");
            }

            if (string.IsNullOrWhiteSpace(restaurantId.ToString()))
            {
                throw new BussinessRuleValidationExeption("Restaurant is required value for reservation");
            }

            if (string.IsNullOrWhiteSpace(tableId.ToString()))
            {
                throw new BussinessRuleValidationExeption("Table is required value for reservation");
            }

            if (string.IsNullOrWhiteSpace(numOfPeople.ToString()))
            {
                throw new BussinessRuleValidationExeption("Number of people is required value for reservation");
            }

            if(string.IsNullOrWhiteSpace(durationFrom.ToString())) 
            {
                throw new BussinessRuleValidationExeption("Reservation beginning time is required");    
            }

            if (string.IsNullOrWhiteSpace(durationTo.ToString()))
            {
                throw new BussinessRuleValidationExeption("Reservation ending time is required");
            }

            if(durationFrom>= durationTo)
            {
                throw new BussinessRuleValidationExeption("Reservation cant end before it starts");
            }

            return new Reservation(Guid.NewGuid(), userId, restaurantId, tableId, numOfPeople, EStatusReservation.Active, durationFrom, durationTo);
        }

        private bool SetStatusCancelled()
        {
            if(this.Status == EStatusReservation.Active)
            {
                this.Status = EStatusReservation.Cancelled;

                return true;
            }

            if(this.Status == EStatusReservation.Completed)
            {
                throw new BussinessRuleValidationExeption("Reservation cant be set to cannceled after it is Completed");
            }

            if(this.Status == EStatusReservation.Cancelled)
            {
                throw new BussinessRuleValidationExeption("Reservation is already canceled");
            }

            else return false;
            
        }

        private bool SetStatusCompleted()
        {
            if (this.Status == EStatusReservation.Active)
            {
                this.Status = EStatusReservation.Completed;

                return true;
            }

            if (this.Status == EStatusReservation.Completed)
            {
                throw new BussinessRuleValidationExeption("Reservation is already completed");
            }

            if (this.Status == EStatusReservation.Cancelled)
            {
                throw new BussinessRuleValidationExeption("Reservation that was cancelled cant be set to 'Completed'");
            }

            else return false;
        }
    }
}