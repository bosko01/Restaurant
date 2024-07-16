namespace Api.Data.DTOs.Table
{
    public class ReadTableDto
    {
        public Guid Id { get; set; }

        public Guid RestaurantId { get; set; }

        public int Seats { get; set; }
    }
}