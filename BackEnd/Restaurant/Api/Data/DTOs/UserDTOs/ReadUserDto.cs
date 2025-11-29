namespace Api.Data.DTOs.UserDTOs
{
    public class ReadUserDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string? Image { get; set; } = string.Empty;

    }
}