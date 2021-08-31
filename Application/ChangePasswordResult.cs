using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application
{
    public class ChangePasswordResult
    {
        public bool Success { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }
}
