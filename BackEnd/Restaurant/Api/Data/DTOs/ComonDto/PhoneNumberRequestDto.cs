using System.ComponentModel.DataAnnotations;

namespace Api.Data.DTOs.ComonDto
{
    public class PhoneNumberRequestDto
    {
        [Required(AllowEmptyStrings = false)]
        public string CountryCode { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false)]
        public string Number { get; set; } = string.Empty;
    }
}