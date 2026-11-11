using System.ComponentModel.DataAnnotations;

namespace APPC.Models.Auth
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [MaxLength(100,ErrorMessage = "Email cannot exceed 100 character")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, ErrorMessage = "Password cannot exceed 100 characters.")]
        public string Password { get; set; }
    }
}
