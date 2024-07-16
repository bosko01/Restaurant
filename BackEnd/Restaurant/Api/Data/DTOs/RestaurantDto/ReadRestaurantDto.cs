namespace Api.Data.DTOs.RestaurantDto
{
    public class ReadRestaurantDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string CountryCode { get; set; } = string.Empty;

        public string Number { get; set; } = string.Empty;

        public string Menu { get; set; } = string.Empty;

        public TimeOnly WorkingHoursFrom { get; set; }

        public TimeOnly WorkingHoursTo { get; set; }
    }
}