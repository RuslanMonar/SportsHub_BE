using System.ComponentModel;
using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
        [DefaultValue(false)]
        public bool IsBlocked { get; set; }
    }
}