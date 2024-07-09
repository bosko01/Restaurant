using Common.Exceptions.RegexValidation;
using System.ComponentModel.DataAnnotations;

namespace Api.Data.DTOs.ComonDto
{
    public class PhoneNumberRequestDto
    {
        [Required(AllowEmptyStrings = false)]
        public string CountryCode { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false)]
        [RegularExpression(RegexValidation.NumericsOnly, ErrorMessage = "Invalid characters used in Number")]
        public string Number { get; set; } = string.Empty;
    }
}