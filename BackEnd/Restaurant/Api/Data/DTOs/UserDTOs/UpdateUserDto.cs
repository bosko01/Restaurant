using Common.Exceptions.RegexValidation;
using System.ComponentModel.DataAnnotations;

namespace Api.Data.DTOs.UserDTOs
{
    public class UpdateUserDto
    {
        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false)]
        public string CountryCode { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false)]
        [RegularExpression(RegexValidation.NumericsOnly, ErrorMessage = "Invalid Values for phone number")]
        public string Number { get; set; } = string.Empty;
    }
}