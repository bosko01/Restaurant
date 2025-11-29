using System.ComponentModel.DataAnnotations;

namespace Api.Data.DTOs.Reservation
{
    public class CreateReservationDto
    {
        [Required(AllowEmptyStrings = false)]
        public Guid UserId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public Guid RestaurantId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public Guid TableId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public int NumberOfPeople { get; set; }

        [Required(AllowEmptyStrings = false)]
        public TimeOnly DurationFrom { get; set; }

        [Required(AllowEmptyStrings = false)]
        public TimeOnly DurationTo { get; set; }
    }
}
