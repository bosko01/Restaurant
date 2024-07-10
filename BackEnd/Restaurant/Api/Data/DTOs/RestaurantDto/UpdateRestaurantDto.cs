using Common.Exceptions.RegexValidation;
using System.ComponentModel.DataAnnotations;

namespace Api.Data.DTOs.RestaurantDto
{
    public class UpdateRestaurantDto
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
        public string CountryCode { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false)]
        public string Menu { get; set; } = string.Empty;
    }
}