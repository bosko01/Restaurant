using Api.Data.DTOs.ComonDto;
using Common.Validation.RegexValidation;
using System.ComponentModel.DataAnnotations;

namespace Api.Data.DTOs.UserDTOs
{
    public class CreateUserDto
    {
        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false)]
        [RegularExpression(RegexValidation.Email, ErrorMessage = "Invalid user Email")]
        public string Email { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false)]
        [MaxLength(15, ErrorMessage = "Password cannot exceed 15 characters.")]
        // [RegularExpression(RegexValidation.Password, ErrorMessage = "Invalid password")]
        public string Password { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false)]
        public PhoneNumberRequestDto PhoneNumber { get; set; } = default!;
    }
}