using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class ContactUsDto
    {
        [Required]
        public string FirstName { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }
        
        public string Message { get; set; }
    }
}
