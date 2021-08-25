using System.Collections.Generic;

namespace Application
{
    public interface IResult
    {
        public IEnumerable<string> Errors { get; set; }

        public bool Success { get; set; }
    }
}
