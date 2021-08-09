using System.Collections.Generic;

namespace API.DTOs
{
    public class UserDto
    {
        //info that returns to the user after successfull login/refister
       
        public string Token { get; set; }

        public string Image { get; set; }

        public IEnumerable<string> Errors { get; set; }

    }
}