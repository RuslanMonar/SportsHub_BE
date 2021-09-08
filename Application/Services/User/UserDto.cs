using System.ComponentModel.DataAnnotations;

namespace Application.Services.User
{
    public class UserDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public bool IsAdmin { get; set; }
    }
}
