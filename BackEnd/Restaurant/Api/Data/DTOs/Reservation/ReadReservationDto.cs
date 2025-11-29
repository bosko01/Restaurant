namespace Api.Data.DTOs.Reservation
{
    public class ReadReservationDto
    {
        public Guid ReservationId { get; set; }

        public string UserFullName { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string RestaurantName { get; set; } = string.Empty;

        public string RestaurantEmail { get; set; } = string.Empty;

        public string Price { get; set; } = string.Empty;

        public int NumberOfPeople { get; set; }

        public TimeOnly From { get; set; }

        public TimeOnly To { get; set; }
    }
}
