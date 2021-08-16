using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class UserGoogleDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}
