using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class UserGoogleDto
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string ImageUrl { get; set; }
    }
}
