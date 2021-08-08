using System.Collections.Generic;

namespace Application
{
    public class AuthResult
    {
        public string Token { get; set; }

        public string Username { get; set; }

        public bool Success { get; set; }

        

        public IEnumerable<string> Errors { get; set; }
    }
}