using System.ComponentModel.DataAnnotations;

namespace Application
{
    public class UpdateToAdminResult
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
    }
}
