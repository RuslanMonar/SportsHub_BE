using System.Collections.Generic;

namespace API.DTOs
{
    public class AuthDto
    {
        //info that returns to the user after successfull login/register
       
        public string Token { get; set; }

        public IEnumerable<string> Errors { get; set; }

    }
}