using System.Collections.Generic;

namespace Application
{
    public class StandartResult: IResult
    {
        public IEnumerable<string> Errors { get; set; }

        public bool Success { get; set; }
    }
}
