using System.Collections.Generic;
using Domain;

namespace API.DTOs.Admin.Users
{
    public class SearchUserDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
    }
}