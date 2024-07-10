using Api.Data.DTOs.ComonDto;
using Common.Exceptions.RegexValidation;
using System.ComponentModel.DataAnnotations;

namespace Api.Data.DTOs.RestaurantDto
{
    public class CreateRestaurantDto
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false)]
        public string Location { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false)]
        [RegularExpression(RegexValidation.Email, ErrorMessage = "Invalid user Email")]
        public string Email { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false)]
        public PhoneNumberRequestDto PhoneNumberRequest { get; set; } = new();

        [Required(AllowEmptyStrings = false)]
        public string Menu { get; set; } = string.Empty;
    }
}