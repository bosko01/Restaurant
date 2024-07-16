using System.ComponentModel.DataAnnotations;

namespace Api.Data.DTOs.UserCredentials
{
    public class UpdateUserCredentialsDto
    {
        [Required(AllowEmptyStrings = false)]
        [MaxLength(15, ErrorMessage = "Password cannot exceed 15 characters.")]
        public string Password { get; set; } = string.Empty;
    }
}