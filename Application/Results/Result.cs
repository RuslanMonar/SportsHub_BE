using System.Collections.Generic;

namespace Application
{
    public class Result
    {
        public IEnumerable<string> Errors { get; set; }

        public bool Success { get; set; }
    }
}
