using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class ForgotPasswordDto
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
    }
}